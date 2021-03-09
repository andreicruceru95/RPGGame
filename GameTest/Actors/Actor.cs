using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Actor
    {
        protected Image Image { get; set; }
        protected Vector2 Velocity { get; set; }
        protected float MoveSpeed { get; set; }
        protected bool IsAlive { get; set; }

        protected Vector2 Origin;
        protected Vector2 Position;

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
