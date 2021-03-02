using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Sprite : ICloneable
    {
        public Texture2D Texture;
        public float Rotation;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;
        public float RotationVelocity;
        public float LinearVelocity;
        public Sprite parent;
        public float LifeSpan = 0f;
        public bool IsRemoved;

        public Sprite(Texture2D Texture)
        {
            this.Texture = Texture;
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);            
        }
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, 1, SpriteEffects.None, 0);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
