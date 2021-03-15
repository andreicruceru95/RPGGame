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
    /// The player class is a virtual representation of a game user.
    /// </summary>
    public class Player : Actor
    {
        public Vector2 Velocity;
        public Vector2 PreviousPosition;
        public float RunSpeed { get; set; }
        public double Energy { get; set; }
        public double Health { get; set; } = 1000;

        /// <summary>
        /// Initialize player value. This can also be done inside the xml file.
        /// </summary>
        public Player()
        {            
            Velocity = Vector2.Zero;
            RunSpeed = 5.0f;
            PreviousPosition = Vector2.Zero;
            KeyResponse.Instance.Actor = this;
            AutoMove.Instance.Actor = this;
        }        
        /// <summary>
        /// Load any content useful for the player class.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
        }
        /// <summary>
        /// Unload any content that the player does not need. This will free up memory.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update the player's atribute and other player-related things.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Image.IsActive = true;

            KeyResponse.Instance.Update(gameTime);
            AnimationManager.Instance.SetFrame(this);

            // if is not moving
            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;
            Image.Position += Velocity;

            base.Update(gameTime);
            SetEnergy(gameTime);            
        }
        private void SetEnergy(GameTime gameTime)
        {
            Energy += gameTime.ElapsedGameTime.TotalSeconds * 0.2;
            if (Energy > 200)
                Energy = 200;
        }

        /// <summary>
        /// Draw on the screen using the graphics object.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {            
            base.Draw(spriteBatch);
        }        
    }
}
