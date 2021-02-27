﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameTest
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;
        string state;

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }
        public void UnloadCotent()
        {

        }
        public void Update(GameTime gameTime, ref Player player)
        {
            if (state == "Solid")
            {
                Rectangle tileRect = new Rectangle((int)Position.X, (int)Position.Y, sourceRect.Width, sourceRect.Height);
                Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y,
                    player.Image.SourceRect.Width, player.Image.SourceRect.Height);
                //Check for colision
                if(playerRect.Intersects(tileRect))
                {
                    //if colision is detected, set the player to a obj rectangle x - player rectangle width/height
                    if (player.Velocity.X < 0)
                        player.Image.Position.X = tileRect.Right;

                    else if (player.Velocity.X > 0)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;

                    else if (player.Velocity.Y < 0)
                        player.Image.Position.Y = tileRect.Bottom;

                    else
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;

                    player.Velocity = Vector2.Zero;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
