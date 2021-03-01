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
        public Vector2 Position = Vector2.Zero;

        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        public float RunSpeed;
        public float Energy { get; set; } = 100000;
        
        public int xCoordinate { get; set; } = 1;
        public int yCoordinate { get; set; } = 1;
        public Player()
        {
            Position = new Vector2(xCoordinate, yCoordinate);
            Velocity = Vector2.Zero;
            RunSpeed = 5.0f;
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
                    if (InputManager.Instance.KeyDown(Keys.Space))
                        Velocity.Y = (MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed;                    
                    else
                        Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;     

                    Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
                else if (InputManager.Instance.KeyDown(Keys.W))
                {
                    if(InputManager.Instance.KeyDown(Keys.Space))
                        Velocity.Y = -((MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed);
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
                    if(InputManager.Instance.KeyDown(Keys.Space))
                        Velocity.X = (MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed;
                    else
                        Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                }
                else if (InputManager.Instance.KeyDown(Keys.A))
                { 
                    if(InputManager.Instance.KeyDown(Keys.Space))
                        Velocity.X = -((MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + RunSpeed);

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
                ScreenManager.Instance.SetCameraView(false);
            if (InputManager.Instance.KeyPressed(Keys.V))
                ScreenManager.Instance.SetCameraView(true);
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
            //add actions
            Move(gameTime);

            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;
            Image.Update(gameTime);
            Image.Position += Velocity;
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

        
    }
}
