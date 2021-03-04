using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameTest
{
    /// <summary>
    /// This class will define the texture layers.
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// A list of tiles set as a row.
        /// </summary>
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;
            public TileMap()
            {
                Row = new List<string>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tile;
        public Image Image;
        public string SolidTiles, OverlayTiles;
        List<Tile> underlayTiles, overlayTiles, notWalkableTiles;
        
        public List<Tile> NotWalkableTiles
        {
            get { return notWalkableTiles; }
        }
        public List<Tile> UnderlayTiles
        {
            get { return underlayTiles; }
        }       
        string state;
        SpriteFont font;

        /// <summary>
        /// Initialize layer.
        /// </summary>
        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            notWalkableTiles = new List<Tile>();
            SolidTiles = OverlayTiles = String.Empty;
            font = ScreenManager.Instance.Font;
        }
        /// <summary>
        /// Load tiles
        /// </summary>
        /// <param name="tileDimensions"></param>
        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();
            Vector2 position = -tileDimensions;

            //split the xml rows and assign the image direction for each one
            foreach(string row in Tile.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y += tileDimensions.Y;
                foreach(string s in split)
                {                    
                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X;
                        if (!s.Contains("x"))
                        {
                            state = "Passive";
                            Tile tile = new Tile();

                            string str = s.Replace("[", String.Empty);
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));
                            if (SolidTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                            {
                                state = "Solid";
                                notWalkableTiles.Add(tile);
                            }

                            tile.LoadContent(position, new Rectangle(
                                value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y,
                                (int)tileDimensions.X, (int)tileDimensions.Y), state);

                            if (OverlayTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                                overlayTiles.Add(tile);
                            else
                                underlayTiles.Add(tile);
                        }
                    }

                }
            }            
        }
        /// <summary>
        /// unload content
        /// </summary>
        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        /// <summary>
        /// update the map, set tiles under/above player
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="player"></param>
        public void Update(GameTime gameTime, Player player)
        {
            foreach(Tile tile in underlayTiles)
                tile.Update(gameTime, player);

            foreach (Tile tile in overlayTiles)
                tile.Update(gameTime, player);

        }
        /// <summary>
        /// draw tiles under/above player
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="drawType"></param>
        public void Draw(SpriteBatch spriteBatch, string drawType)
        {
            List<Tile> tiles;
            if (drawType == "Underlay")
                tiles = underlayTiles;              
            
            else
                tiles = overlayTiles;
            foreach(Tile tile in tiles)
            {
                //Set position to tilesheet
                Image.Position = tile.Position;
                //set source to tilesheet
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }            
        }
    }
}
