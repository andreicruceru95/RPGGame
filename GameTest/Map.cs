using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameTest
{
    public class Map
    {
        [XmlElement("Layer")]
        public List<Layer> Layer;
        public Vector2 TileDimensions;

        public Map()
        {
            Layer = new List<Layer>();
            TileDimensions = new Vector2(32, 32);
        }

        public void LoadContent()
        {
            foreach (Layer l in Layer)
                l.LoadContent(TileDimensions);
        }
        public void UnloadContent()
        {
            foreach (Layer l in Layer)
                l.UnloadContent();
        }
        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (Layer l in Layer)
                l.Update(gameTime, ref player);
        }
        public void Draw(SpriteBatch spriteBatch,string drawType)
        {
            foreach (Layer l in Layer)
                l.Draw(spriteBatch, drawType);
        }
    }
}
