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

        public Player()
        {            
            Velocity = Vector2.Zero;
            RunSpeed = 5.0f;
            moveManager = new MovementHandler(this);
        }
        private void SetCamera()
        {
            if (InputManager.Instance.KeyPressed(Keys.C)) Camera.Instance.Follow = true;

            if (InputManager.Instance.KeyPressed(Keys.V)) Camera.Instance.Follow = false;
        }
        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

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

        public void Draw(SpriteBatch spriteBatch)
        {            
            Image.Draw(spriteBatch);
            moveManager.Draw(spriteBatch);
        }        
    }
}
