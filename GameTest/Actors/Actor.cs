using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public abstract class Actor
    {
        public Image Image { get; set; }
        //public Vector2 Velocity { get; set; }
        public float MoveSpeed { get; set; }
        public bool IsAlive { get; set; }

        public Vector2 Origin;
        public Vector2 Position;

        public Actor()
        {
            Origin = Vector2.Zero;
        }
        //replace with actor
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
