﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameTest
{
    /// <summary>
    /// Tiles are at the lowest of the map.
    /// </summary>
    public class Tile : ISolid
    {
        Vector2 position;
        Rectangle sourceRect;
        Vector2 xyCoord;
        
        //walking cost from the start node
        public int GCost { get; set; }
        //estimate distance to end node
        public int HCost { get; set; }
        //GCost + HCost
        public int FCost { get; set; }
        public Tile CameFromTile { get; set; }
        //define colision
        string state;
        public string State { get; set; }
        //{
        //    get { return state; }
        //}
        
        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 XYCoord
        {
            get { return xyCoord; }
        }
        public Vector2 TileCentre
        {
            get
            {
                return position + new Vector2(16, 16);
            }
        }     

        public void CalculateFCost()
        {
            FCost = GCost + HCost;
        }
        /// <summary>
        /// load tiles
        /// </summary>
        /// <param name="position"></param>
        /// <param name="sourceRect"></param>
        /// <param name="state"></param>
        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
            xyCoord = new Vector2(position.X / sourceRect.Width, position.Y / sourceRect.Height);
        }
        public void UnloadCotent()
        { }
        /// <summary>
        /// Update game based on tile state.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="player"></param>
        public void Update(GameTime gameTime)//, Player player)
        {
            if (state == "Solid")// && player.KeyboardMode)
            {                
                Collision.Instance.CheckCollision(this);
                //Rectangle tileRect = new Rectangle((int)Position.X, (int)Position.Y, sourceRect.Width, sourceRect.Height);
                //Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y,
                //    player.Image.SourceRect.Width, player.Image.SourceRect.Height);
                ////Check for colision
                //if(playerRect.Intersects(tileRect))
                //{                    
                //    //if colision is detected, set the player to a obj rectangle x - player rectangle width/height
                //    if (player.Velocity.X < 0)
                //        player.Image.Position.X = tileRect.Right;

                //    else if (player.Velocity.X > 0)
                //        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;

                //    else if (player.Velocity.Y < 0)
                //        player.Image.Position.Y = tileRect.Bottom;

                //    else
                //        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;

                //    player.Velocity = Vector2.Zero;
                //}
            }
        }        
    }
}
