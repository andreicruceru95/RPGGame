using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Actor represents a moving game objects such as a player or monster.
    /// </summary>
    public abstract class Actor : ISolid
    {
        public Image Image { get; set; }
        public float MoveSpeed { get; set; }
        public bool IsAlive { get; set; }
               
        public Vector2 Position
        {
            get
            {
                return Image.Position;
            }
        }

        public Rectangle SourceRect
        {
            get
            {
                return Image.SourceRect;
            }
        }

        public Actor()
        {}
        
        public virtual void LoadContent()
        {
            Image.LoadContent();
        }
        public virtual void UnloadContent()
        {
            Image.UnloadContent();
        }
        public virtual void Update(GameTime gameTime)
        {            
            Image.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
