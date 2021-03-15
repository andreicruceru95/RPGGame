using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Xml.Serialization;

namespace GameTest
{
    public class Monster : Actor
    {
        public Vector2 Velocity { get; set; }
        public Vector2 InitialPosition { get; set; }
        public float ActiveArea = 220f;
        public bool IsEngaged;
        public Monster()
        {             
            Velocity = Vector2.Zero;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            InitialPosition = Image.Position;
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            Image.IsActive = true;

            AutoMove.Instance.Update(gameTime, this);
            AnimationManager.Instance.SetFrame(this);

            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;            
            Image.Position += Velocity;

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            AutoMove.Instance.Draw(spriteBatch);
        }
    }
}
