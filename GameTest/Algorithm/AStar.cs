using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTest
{   
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
        /// initialize the algorithm
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
        /// Start the algorithm
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

                tile.GCost = 0;
                tile.GCost = int.MaxValue;
                tile.CalculateFCost();
                tile.CameFromTile = null;

            }
            startTile.GCost = 0;
            //making sure is taking the centre of the tile
            startTile.HCost = CalculateDistanceCost(startTile.Position, endTile.Position);
            startTile.CalculateFCost();

            return Cycle(endTile);
        }

        /// <summary>
        /// The main Algorithm
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
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
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
        /// Calculate the path.
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
        /// sort the open list and get the tile with the lowest cost
        /// </summary>        
        private Tile GetLowestFCostTile(List<Tile> pathTileList)
        {
            var list = pathTileList.OrderBy(T => T.FCost).ToList();

            return list[0];
        }
       
        /// <summary>
        /// return the distance cost between 2 points
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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
        /// Return the tile at a given location
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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
        /// Convet the list of tiles to a list of adresses.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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
