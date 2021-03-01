using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Bullet : Sprite
    {
        public float timer;
        public Bullet(Texture2D Texture)
            : base(Texture)
        {

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > LifeSpan)
                IsRemoved = true;

            Position += Direction * LinearVelocity;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }        

        
    }
}
