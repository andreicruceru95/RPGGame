using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class MovementHandler
    {
        float x = ScreenManager.Instance.Dimensions.X / 2;
        float y = ScreenManager.Instance.Dimensions.Y / 2;

        List<Vector2> vectorPath;
        MouseState mouse;
        int currentIndexPath;
        Player player;
        bool drawPath;
        public MovementHandler(Player player)
        {
            //vectorPath = new List<Vector2>();
            mouse = new MouseState();
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            GetMouseInput();
            Move(gameTime);
            HandleMovement(gameTime);
            SetFrame();
            player.Energy += gameTime.ElapsedGameTime.TotalSeconds * 0.2;
            if (player.Energy > 200)
                player.Energy = 200;

            // if is not moving
            //if (player.Velocity.X == 0 && player.Velocity.Y == 0)
            //    player.Image.IsActive = false;
            player.Image.Update(gameTime);
            player.Image.Position += player.Velocity;

            if (InputManager.Instance.KeyPressed(Keys.Q))
                drawPath = true;
        }

        private void GetMouseInput()
        {
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Vector2 mouseTarget = player.Image.Position + new Vector2(mouse.X - x, mouse.Y - y);
                if (!(mouse.X < 0 || mouse.X > ScreenManager.Instance.Dimensions.X ||
                   mouse.Y < 0 || mouse.Y > ScreenManager.Instance.Dimensions.Y
                   || mouseTarget.X < 0 || mouseTarget.Y < 0 || mouseTarget.X > 1408 || mouseTarget.Y > 1696))
                    SetTarget(mouseTarget);
            }
        }

        void HandleMovement(GameTime gameTime)
        {
            if (vectorPath != null)
            {
                Vector2 targetPosition = vectorPath[currentIndexPath];
                if (Vector2.Distance(player.Image.Position, targetPosition) > 10f)
                {
                    Vector2 moveDir = Vector2.Normalize(targetPosition - player.Image.Position);
                    float distance = Vector2.Distance(player.Image.Position, targetPosition);
                    
                    player.Velocity = moveDir * player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;                    
                }
                else
                {
                    currentIndexPath++;

                    if (currentIndexPath >= vectorPath.Count)
                    {
                        player.Velocity = Vector2.Zero;
                        vectorPath = null;
                    }
                }
            }
            else
                player.KeyboardMode = true;
        }

        private void SetTarget(Vector2 target)
        {
            player.KeyboardMode = false;

            currentIndexPath = 0;
            vectorPath = AStar.Instance.FindPath(ScreenManager.Instance.CurrentMap, player.Image.Position, target);

            if (vectorPath != null && vectorPath.Count > 1)
                vectorPath.RemoveAt(0);
        }
        /// <summary>
        /// Move player. 
        /// </summary>
        private void Move(GameTime gameTime)
        {
            if (player.Velocity.X == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.S))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && player.Energy > 0)
                    {
                        player.Velocity.Y = (player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + player.RunSpeed;
                        player.Energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }
                    else
                        player.Velocity.Y = player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    player.Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
                else if (InputManager.Instance.KeyDown(Keys.W))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && player.Energy > 0)
                    {
                        player.Velocity.Y = -((player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + player.RunSpeed);
                        player.Energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }
                    else
                        player.Velocity.Y = -player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    player.Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                }
                else
                    player.Velocity.Y = 0;
            }

            if (player.Velocity.Y == 0)
            {
                if (InputManager.Instance.KeyDown(Keys.D))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && player.Energy > 0)
                    {
                        player.Velocity.X = (player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + player.RunSpeed;
                        player.Energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }
                    else
                        player.Velocity.X = player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    player.Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                }
                else if (InputManager.Instance.KeyDown(Keys.A))
                {
                    if (InputManager.Instance.KeyDown(Keys.Space) && player.Energy > 0)
                    {
                        player.Velocity.X = -((player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) + player.RunSpeed);
                        player.Energy -= (gameTime.ElapsedGameTime.TotalSeconds) * 5;
                    }

                    else
                        player.Velocity.X = -player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    player.Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                }
                else
                    player.Velocity.X = 0;
            }
                        
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (vectorPath != null && drawPath)
            {
                for (int i = 0; i < vectorPath.Count - 1; i++)
                {
                    spriteBatch.DrawLine(vectorPath[i], vectorPath[i + 1], Color.Red);
                }
                //path.Clear();
            }
        }
        private void SetFrame()
        {
            if (player.Velocity.X == 0 && player.Velocity.Y == 0)
            {
                player.Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                player.Image.IsActive = false;
            }
            if (player.Velocity.X > 0)
                player.Image.SpriteSheetEffect.CurrentFrame.Y = 2;
            else
                player.Image.SpriteSheetEffect.CurrentFrame.Y = 1;

            if (player.Velocity.Y > 0 && player.Velocity.X == 0)
                player.Image.SpriteSheetEffect.CurrentFrame.Y = 0;

            else if (player.Velocity.Y < 0 && player.Velocity.X == 0)
                player.Image.SpriteSheetEffect.CurrentFrame.Y = 3;
            
        }
    }
}
