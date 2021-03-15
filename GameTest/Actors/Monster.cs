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
    /// <summary>
    /// Monster is a protagonist of the game.
    /// The monster has a virtual circle active area where in which it will make random movements.
    /// If the player steps into this area, it will become a target for the monster.
    /// </summary>
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
        /// <summary>
        /// Load Content
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            InitialPosition = Image.Position;
        }
        /// <summary>
        /// Unload Content
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// Update content
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Image.IsActive = true;

            AutoMove.Instance.Update(gameTime, this);
            AnimationManager.Instance.SetFrame(this);
            Collision.Instance.CheckCollision(this);

            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;            
            Image.Position += Velocity;

            base.Update(gameTime);
        }
        /// <summary>
        /// Draw content
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            AutoMove.Instance.Draw(spriteBatch);
        }
    }
}
