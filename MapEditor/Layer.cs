using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MapEditor
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;
        }
        Vector2 tileDimensions;
        List<List<Vector2>> tileMap;

        public TileMap TileLayout;
        public Image Image;

        public void Initialize(ContentManager content, Vector2 tileDimensions)
        {
            //Add a rows to our tile map 
            foreach(string row in TileLayout.Row)
            {
                string[] split = row.Split(']');
                List<Vector2> tempTileMap = new List<Vector2>();

                foreach(string s in split)
                {
                    int value1, value2;
                    //x means second layer
                    if (!s.Contains('x'))
                    {
                        string str = s.Replace("[", String.Empty);
                        value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));
                    }
                    else
                    {
                        value1 = value2 = -1;
                    }

                    tempTileMap.Add(new Vector2(value1, value2));
                }
                tileMap.Add(tempTileMap);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
