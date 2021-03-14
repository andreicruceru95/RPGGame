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
    public class Player
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed { get; set; }
        public float RunSpeed { get; set; }
        public bool KeyboardMode { get; set; }
        public double Energy { get; set; }
        public double Health { get; set; } = 1000;

        public double GetEnergy
        {
            get{ return Math.Round(Energy, 1); }
        }
        MovementHandler moveManager;

        /// <summary>
        /// Initialize player value. This can also be done inside the xml file.
        /// </summary>
        public Player()
        {            
            Velocity = Vector2.Zero;
            RunSpeed = 5.0f;
            moveManager = new MovementHandler(this);
        }
        /// <summary>
        /// Change the camera based on player input.
        /// </summary>
        private void SetCamera()
        {
            if (InputManager.Instance.KeyPressed(Keys.C)) Camera.Instance.Follow = true;

            if (InputManager.Instance.KeyPressed(Keys.V)) Camera.Instance.Follow = false;
        }
        /// <summary>
        /// Load any content useful for the player class.
        /// </summary>
        public void LoadContent()
        {
            Image.LoadContent();
        }
        /// <summary>
        /// Unload any content that the player does not need. This will free up memory.
        /// </summary>
        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        /// <summary>
        /// Update the player's atribute and other player-related things.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Image.IsActive = true;
            SetCamera();
            moveManager.Update(gameTime);
            // if is not moving
            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;
            Image.Update(gameTime);
            Image.Position += Velocity;
        }

        /// <summary>
        /// Draw on the screen using the graphics object.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {            
            Image.Draw(spriteBatch);
            moveManager.Draw(spriteBatch);
        }        
    }
}
