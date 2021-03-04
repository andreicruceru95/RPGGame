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
        List<Vector2> vectorPath;
        int currentIndexPath;
        float distanceBefore = 0;
        public bool keyboardMode { get; set; }


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
            if (InputManager.Instance.KeyPressed(Keys.C)) Camera.Instance.Follow = true;

            if (InputManager.Instance.KeyPressed(Keys.V)) Camera.Instance.Follow = false;

            //if (InputManager.Instance.KeyPressed(Keys.Z))
            //    Image.ActivateEffect("FadeEffect");
            //if (InputManager.Instance.KeyPressed(Keys.B))
            //    Image.DeactivateEffect("FadeEffect");
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
            HandleMovement(gameTime);
            // if is not moving
            if (Velocity.X == 0 && Velocity.Y == 0)
                Image.IsActive = false;
            Image.Update(gameTime);
            Image.Position += Velocity;
            energy += gameTime.ElapsedGameTime.TotalSeconds * 0.2;
            if (energy > 2000)
                energy = 2000;
            if (InputManager.Instance.KeyPressed(Keys.P))
                keyboardMode = true;
            if (InputManager.Instance.KeyPressed(Keys.Q))
            {
                Vector2 target = new Vector2(920, 1232);
                SetTarget(target);//(mouse.X - Image.Position.X, mouse.Y - Image.Position.Y);
                //Velocity = MovementHandler.Instance.SetTarget(gameTime, Image.Position, target, MoveSpeed);
            }
        }

        void HandleMovement(GameTime gameTime)
        {
            if (vectorPath != null)
            {


                Vector2 targetPosition = vectorPath[currentIndexPath];
                if (Vector2.Distance(Image.Position, targetPosition) > 10f)
                {
                    Vector2 moveDir = Vector2.Normalize(targetPosition - Image.Position);
                    float distance = Vector2.Distance(Image.Position, targetPosition);

                    //SetMoveVector(moveDir);
                    if (distanceBefore == distance)
                        Velocity -= moveDir * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        Velocity = moveDir * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    distanceBefore = distance;

                    if (InputManager.Instance.KeyPressed(Keys.T))
                    {
                        vectorPath = null;
                        Velocity = Vector2.Zero;
                    }
                }
                else
                {
                    currentIndexPath++;
                    if (currentIndexPath >= vectorPath.Count)
                    {
                        Velocity = Vector2.Zero;
                        vectorPath = null;
                    }
                }
            }
        }

        private void SetTarget(Vector2 target)
        {
            currentIndexPath = 0;
            vectorPath = AStar.Instance.FindPath(ScreenManager.Instance.CurrentMap, Image.Position, target);

            if (vectorPath != null && vectorPath.Count > 1)
                vectorPath.RemoveAt(0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            Image.Draw(spriteBatch);

            if (vectorPath != null)
            {
                for(int i = 0; i< vectorPath.Count - 1; i++)
                {
                    spriteBatch.DrawLine(vectorPath[i], vectorPath[i+1], Color.Red);
                }
                //path.Clear();
            }
        }

        
    }
}
