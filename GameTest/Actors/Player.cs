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
        public float MoveSpeed;
        float RunSpeed;
        double energy = 20000;
        public double Energy
        {
            get{ return Math.Round(energy, 1); }
        }
                
        public Player()
        {            
            Velocity = Vector2.Zero;
            RunSpeed = 10.0f;           
        }
        /// <summary>
        /// Move player. 
        /// </summary>
        private void Move(GameTime gameTime)
        {
            if (Velocity.X == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.S))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && energy > 0)
                    {
                        Velocity.Y = (MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed;
                        energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }
                    else
                        Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
                else if (InputManager.Instance.KeyDown(Keys.W))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && energy > 0)
                    {
                        Velocity.Y = -((MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed);
                        energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }
                    else
                        Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                }
                else
                    Velocity.Y = 0;
            }

            if (Velocity.Y == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.D))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && Energy > 0)
                    {
                        Velocity.X = (MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed;
                        energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }
                    else
                        Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                }
                else if (InputManager.Instance.KeyDown(Keys.A))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && Energy > 0)
                    {
                        Velocity.X = -((MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed);
                        energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }

                    else
                        Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                }
                else
                    Velocity.X = 0;
            }
        }
        private void SetCamera()
        {
            if (InputManager.Instance.KeyPressed(Keys.C))
                Camera.Instance.Follow = true;
            if (InputManager.Instance.KeyPressed(Keys.V))
                Camera.Instance.Follow = false;
            if (InputManager.Instance.KeyPressed(Keys.Z))
                Image.ActivateEffect("FadeEffect");
            if (InputManager.Instance.KeyPressed(Keys.B))
                Image.DeactivateEffect("FadeEffect");
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
            Move(gameTime);

            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;
            Image.Update(gameTime);
            Image.Position += Velocity;
            energy += gameTime.ElapsedGameTime.TotalSeconds * 0.2;
            if (energy > 2000)
                energy = 2000;
        }
        public void Draw(SpriteBatch spriteBatch)
        {            
            Image.Draw(spriteBatch);
        }

        
    }
}
