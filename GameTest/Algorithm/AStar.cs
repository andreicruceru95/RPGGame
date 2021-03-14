using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTest
{   
    /// <summary>
    /// This is an AI algorithm that will calculate the shortest path between 
    /// any two given vector locations.
    /// </summary>
    public class AStar
    {
        private static AStar instance;

        const int MOVE_STRAIGHT_COST = 32;
        //const int MOVE_DIAGONAL_COST = 45;

        private List<Tile> openList, closedList, solidTiles, neighbourTiles;
        
        Layer layer;
        Vector2 tileDimension;
        int mapWidth, mapHeight;

        public static AStar Instance
        {
            get
            {
                if (instance == null)
                    instance = new AStar();

                return instance;
                    
            }
        }
        /// <summary>
        /// initialize the algorithm.
        /// OpenList will store explorable routes as nodes.
        /// ClosedList will store explored routes as nodes.
        /// NeighbourList will store the neighbours of the current Node.
        /// layer repesents the background layer of the map which contains fully explorable tiles.
        /// solidTiles represents the second layer of the map which contains solid, unexplorable tiles.
        /// </summary>
        /// <param name="newMap"></param>
        public void Initialize(Map newMap)
        {
            Map map = newMap;

            openList = new List<Tile>();
            closedList = new List<Tile>();
            neighbourTiles = new List<Tile>();

            layer = map.Layer[0];
            solidTiles = map.Layer[1].NotWalkableTiles;            

            tileDimension = new Vector2(layer.UnderlayTiles[0].SourceRect.Width, 
                        layer.UnderlayTiles[0].SourceRect.Height);
            mapHeight = layer.Tile.Row.Count;
            mapWidth = layer.UnderlayTiles.Count / mapHeight;        
        }

        /// <summary>
        /// Start the algorithm.
        /// Convert the vector location to the coresponding tile on the map.
        /// Add the start tile to the open list as it us the first explorable node.
        /// Set the tile state to 'solid' for each tile that is in both first and second map layer.
        /// For each tile: set the GCost (distance between tile and end location) to infinite,
        /// calculate the FCost(total cost), and set previous tile to null. This is done to override any previous values.
        /// </summary>
        public List<Tile> GetPath(Map map,Vector2 start, Vector2 end)
        {
            Initialize(map);            

            Tile startTile = GetTileAtVectorLocation(start);
            Tile endTile = GetTileAtVectorLocation(end);
            openList.Add(startTile);
            
            foreach(Tile tile in layer.UnderlayTiles)
            {
                for (int i = 0; i < solidTiles.Count; i++)
                {
                    if (tile.Position == solidTiles[i].Position)
                    {
                        tile.State = "Solid";
                        break;
                    }
                }
                //initialize the values so no data from last run is stored.
                tile.GCost = int.MaxValue;
                tile.CalculateFCost();
                tile.CameFromTile = null;

            }
            // start tile always has a distance cost of 0
            startTile.GCost = 0;
            //making sure is taking the centre of the tile
            startTile.HCost = CalculateDistanceCost(startTile.Position, endTile.Position);
            startTile.CalculateFCost();

            return Cycle(endTile);
        }

        /// <summary>
        /// The main Algorithm.
        /// If the open list is not empty, there are posible routes to explore.
        /// We take the tile with the lowest FCost out of the openList and find each neighbour for it. If any neighbour wasn't 
        /// explored before, we add it to the open list. If the neighbour has a TentativeCost lower than the current tiles GCost,
        /// than that neighbour will become our new current node.
        /// The process is repeated until current tile and end tile are the same, or the open list is empty(no route was found).
        /// If this process is terminated with success then we create a list of adresses by looking at 'comeFromTile' attribute.
        /// </summary>
        /// <returns></returns>
        private List<Tile> Cycle(Tile endTile)
        {
            while(openList.Count > 0)
            {
                Tile currentTile = GetLowestFCostTile(openList);
                if (currentTile == endTile) return CalculatePath(endTile);//reached target

                //else add it to closed list
                openList.Remove(currentTile);
                closedList.Add(currentTile);

                foreach(Tile neighbourTile in GetNeighbourList(currentTile))
                {
                    if (closedList.Contains(neighbourTile)) continue;
                    if(neighbourTile.State == "Solid")
                    {
                        closedList.Add(neighbourTile);
                        continue;
                    }

                    int tentativeGCost = neighbourTile.GCost + CalculateDistanceCost(currentTile.Position, neighbourTile.Position);

                    if(tentativeGCost < neighbourTile.GCost)
                    {
                        neighbourTile.CameFromTile = currentTile;
                        neighbourTile.GCost = tentativeGCost;
                        neighbourTile.HCost = CalculateDistanceCost(neighbourTile.Position, endTile.Position);
                        neighbourTile.CalculateFCost();

                        if (!openList.Contains(neighbourTile)) openList.Add(neighbourTile);
                    }
                }
            }
            //out of tiles
            return null;
        }
        /// <summary>
        /// Calculate and return a list of neighbours.
        /// This defines weather we can use digonal neighbours or not.
        /// </summary>
        /// <param name="tile">a given tile</param>
        /// <returns>a list of neighbours for the given tile.</returns>
        private List<Tile> GetNeighbourList(Tile tile)
        {
            neighbourTiles = new List<Tile>();
            //Add all the possible nodes to list
            int tileIndex = GetIndexOfTile(tile.Position);
            List<int> indexes = new List<int>
            {
                // I removed the diagonal neighbours
                //tileIndex - (mapWidth + 1),
                tileIndex - mapWidth,
                //tileIndex - (mapWidth -1),
                tileIndex - 1,
                tileIndex + 1,
                //tileIndex + (mapWidth- 1),
                tileIndex + mapWidth,
                //tileIndex + (mapWidth + 1),
            };
            foreach (int index in indexes)
            {   
                if ((index < 0 || index >= layer.UnderlayTiles.Count)) continue;

                Tile neigbourTile = layer.UnderlayTiles[index];

                if (neigbourTile == null || openList.Contains(neigbourTile) || closedList.Contains(neigbourTile)) continue;
                neighbourTiles.Add(neigbourTile);
            }

            return neighbourTiles;
        }        

        /// <summary>
        /// Calculate the path from end tile to start tile by tracing the 'comeFromTile' attribute.
        /// All this tiles are added to a list and then reversed so the path goes from start-> end.
        /// </summary>
        /// <param name="endTile"></param>
        /// <returns>a list of tile as adresses</returns>
        private List<Tile> CalculatePath(Tile endTile)
        {
            List<Tile> path = new List<Tile>();
            path.Add(endTile);
            Tile currentTile = endTile;

            while(currentTile.CameFromTile != null)
            {
                path.Add(currentTile.CameFromTile);
                currentTile = currentTile.CameFromTile;
            }
            path.Reverse();

            return path;
        }

        /// <summary>
        /// Order the list asscending based on the FCost.
        /// </summary>
        /// <param name="pathTileList">A list of tiles</param>
        /// <returns>the tile in the list with the lowest FCost</returns>
        private Tile GetLowestFCostTile(List<Tile> pathTileList)
        {
            var list = pathTileList.OrderBy(T => T.FCost).ToList();

            return list[0];
        }
       
        /// <summary>
        /// return the distance cost between 2 points
        /// </summary>
        /// <param name="start">starting location</param>
        /// <param name="end">end location</param>
        /// <returns>the moving cost.</returns>
        private int CalculateDistanceCost(Vector2 start, Vector2 end)
        {
            int startX = (int)start.X / (int)tileDimension.X;
            int startY = (int)start.Y / (int)tileDimension.X;
            int endX = (int)end.X / (int)tileDimension.Y;
            int endY = (int)end.Y / (int)tileDimension.Y;

            int diagonalDistanceX = Math.Abs(startX - endX);
            int diagonalDistanceY = Math.Abs(startY - endY);
            //int remaining = Math.Abs(diagonalDistanceX - diagonalDistanceY);
            //amount we can move diagonaly + amount we can move straight
            //return MOVE_DIAGONAL_COST * Math.Min(diagonalDistanceX, diagonalDistanceY) + MOVE_STRAIGHT_COST * remaining;
            return MOVE_STRAIGHT_COST * (diagonalDistanceX + diagonalDistanceY);
            // The first return is for diagonal distances
        }
          
        /// <summary>
        /// Convert a vector type location to a tile type location.
        /// </summary>
        /// <param name="location"></param>
        private Tile GetTileAtVectorLocation(Vector2 location)
        {
            int x = Convert.ToInt16((location.X / tileDimension.X));
            int y = Convert.ToInt16((location.Y / tileDimension.Y));
            if (!(x < 0 || x > mapWidth || y < 0 || y > mapHeight))
                return layer.UnderlayTiles[(mapWidth - 1) * y + (x + y)];
            return null;
        }

        /// <summary>
        /// Return a tile with a given x, y index location location.
        /// </summary>
        /// <param name="location">A vector2 type location</param>
        /// <returns>return the index of a tile that contains the vector location.</returns>
        private int GetIndexOfTile(Vector2 location)
        {
            int x = (int)(location.X / tileDimension.X);
            int y = (int)(location.Y / tileDimension.Y);
            if ((mapWidth -1) * y + (x + y) > layer.UnderlayTiles.Count|| (mapWidth -1) * y + (x + y) < 0)
                return -1;
            else 
                return (mapWidth -1) * y + (x + y);
        }        
        /// <summary>
        /// Convert the list of tile adresses to a list of vector adresses.
        /// </summary>
        /// <param name="map">The map where the player and target are using</param>
        /// <param name="start">the start position as vector</param>
        /// <param name="end">the end position as vector</param>
        /// <returns>return a path of vectors.</returns>
        public List<Vector2> FindPath(Map map, Vector2 start, Vector2 end)
        {
            List<Tile> path = GetPath(map, start, end);
            if (path == null) return null;
            
            else
            {
                List<Vector2> vectorPath = new List<Vector2>();
                foreach(Tile tile in path)
                {
                    vectorPath.Add(tile.TileCentre);
                }
                return vectorPath;
            }
        }
    }
}
