using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Camera
    {
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }
        Player player;
        bool followPlayer;
        float row;
        float col;

        private float currentMouseWheelValue, previousMouseWheelValue, zoom, previousZoom;

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Position = new Vector2(600, 330);
        }


        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
        public void SetPlayer(Player player)
        {
            this.player = player;
        }
        public void SetView(bool followPlayer)
        {
            this.followPlayer = followPlayer;
        }
        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            UpdateVisibleArea();
        }

        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            Position = newPosition;
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;
            if (Zoom < 1f)
            {
                Zoom = 1f;
            }
            if (Zoom > 1.5f)
            {
                Zoom = 1.5f;
            }
        }

        //public void Follow()
        //{
        //    var position = Matrix.CreateTranslation(
        //        -player.Image.Position.X - (player.Image.SourceRect.Width / 2),
        //        -player.Image.Position.Y - (player.Image.SourceRect.Height / 2),
        //        0);
        //    var offset = Matrix.CreateTranslation(640, 360, 0);

        //    Transform = position * offset;
        //}

        public void UpdateCamera(Viewport bounds)
        {
            if (followPlayer)
            {
                //Vector2 newPosition = Vector2.Zero;
                //row += player.Velocity.X;
                //col += player.Velocity.Y;
                //newPosition.X = row - 320;
                //newPosition.Y = col - 180;
                //Position = newPosition;
                var position = Matrix.CreateTranslation(
                -player.Image.Position.X - (player.Image.SourceRect.Width / 2),
                -player.Image.Position.Y - (player.Image.SourceRect.Height / 2),
                0);
                var offset = Matrix.CreateTranslation(640, 360, 0);
                
                Transform = position * offset;                
            }
            else
            {
                Bounds = bounds.Bounds;
                    

                UpdateMatrix();

                Vector2 cameraMovement = Vector2.Zero;
                int moveSpeed;

                if (Zoom > .8f)
                {
                    moveSpeed = 15;
                }
                else if (Zoom < .8f && Zoom >= .6f)
                {
                    moveSpeed = 20;
                }
                else if (Zoom < .6f && Zoom > .35f)
                {
                    moveSpeed = 25;
                }
                else if (Zoom <= .35f)
                {
                    moveSpeed = 30;
                }
                else
                {
                    moveSpeed = 10;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    cameraMovement.Y = -moveSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    cameraMovement.Y = moveSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    cameraMovement.X = -moveSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    cameraMovement.X = moveSpeed;
                }

                previousMouseWheelValue = currentMouseWheelValue;
                currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

                if (currentMouseWheelValue > previousMouseWheelValue)
                {
                    AdjustZoom(.05f);
                    Console.WriteLine(moveSpeed);
                }

                if (currentMouseWheelValue < previousMouseWheelValue)
                {
                    AdjustZoom(-.05f);
                    Console.WriteLine(moveSpeed);
                }

                previousZoom = zoom;
                zoom = Zoom;
                if (previousZoom != zoom)
                {
                    Console.WriteLine(zoom);

                }

                MoveCamera(cameraMovement);
            }
        }
    }
}
