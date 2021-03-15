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
    /// A game map created from multiple stacked layers.
    /// </summary>
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer { get; }
        public Vector2 TileDimensions;
        /// <summary>
        /// Initialzize
        /// </summary>
        public Map()
        {
            //set layers and dimensions for each tile
            Layer = new List<Layer>();
            TileDimensions = new Vector2(32, 32);
        }
        

        /// <summary>
        /// load tiles
        /// </summary>
        public void LoadContent()
        {
            foreach (Layer l in Layer)
                l.LoadContent(TileDimensions);
        }
        /// <summary>
        /// unload each layer
        /// </summary>
        public void UnloadContent()
        {
            foreach (Layer l in Layer)
                l.UnloadContent();
        }
        /// <summary>
        /// update map texture by calling each layer.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="player"></param>
        public void Update(GameTime gameTime)//, Player player)
        {
            foreach (Layer l in Layer)
                l.Update(gameTime);//, player);
        }
        /// <summary>
        /// draw layers(underlay, overlay)
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="drawType"></param>
        public void Draw(SpriteBatch spriteBatch,string drawType)
        {
            foreach (Layer l in Layer)
                l.Draw(spriteBatch, drawType);            
        }
    }
}
