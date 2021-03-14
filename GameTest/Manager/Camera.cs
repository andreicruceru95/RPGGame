using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Camera view of the game using a matrix.
    /// </summary>
    public class Camera
    {
        private static Camera instance;
        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; protected set; }
        public Rectangle VisibleArea { get; protected set; }
        public Matrix Transform { get; protected set; }
        public Player Player { get; set; }
        public Viewport Viewport { get; set; }
        public bool Follow { get; set; }

        private float currentMouseWheelValue, previousMouseWheelValue;
        /// <summary>
        /// Force this class to a singleton. There can only be one camera view (player's perspective)
        /// </summary>
        public static Camera Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }
        }
        /// <summary>
        /// Initialize.
        /// </summary>
        public Camera()
        {
            Bounds = Viewport.Bounds;
            Zoom = 1f;
            Position = new Vector2(640, 360);
        }


        //private void UpdateVisibleArea()
        //{
        //    var inverseViewMatrix = Matrix.Invert(Transform);

        //    var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
        //    var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
        //    var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
        //    var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

        //    var min = new Vector2(
        //        MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
        //        MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
        //    var max = new Vector2(
        //        MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
        //        MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
        //    VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        //}
        /// <summary>
        /// Update camera matrix.
        /// </summary>
        private void UpdateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            //UpdateVisibleArea();
        }
        /// <summary>
        /// Move the camera.
        /// </summary>
        /// <param name="movePosition">distance to move</param>
        public void MoveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = Position + movePosition;
            if (newPosition.Y - (ScreenManager.Instance.Dimensions.Y/2) < 0)
                newPosition.Y = 360;
            else if (newPosition.Y + (ScreenManager.Instance.Dimensions.Y / 2) > 1870)
                newPosition.Y = 1510;
            if (newPosition.X - (ScreenManager.Instance.Dimensions.X / 2) < 0)
                newPosition.X = 640;
            if (newPosition.X + (ScreenManager.Instance.Dimensions.X / 2) > 1670)
                newPosition.X = 1030;

            Position = newPosition;
        }
        /// <summary>
        /// Adjust the camera zoom.
        /// </summary>
        public void AdjustZoom()
        {
            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue) Zoom += 0.05f;            

            if (currentMouseWheelValue < previousMouseWheelValue) Zoom -= 0.05f;
               
            if (Zoom < 1f) Zoom = 1f;
           
            if (Zoom > 1.5f) Zoom = 1.5f;           
        }    
        
        /// <summary>
        /// set the matrix to follow the player.
        /// </summary>
        private void FollowPlayer()
        {
            var position = Matrix.CreateTranslation(
                -Player.Image.Position.X - (Player.Image.SourceRect.Width / 2),
                -Player.Image.Position.Y - (Player.Image.SourceRect.Height / 2),
                0);
            var offset = Matrix.CreateTranslation(640, 360, 0);

            Transform = position * offset; //*Matrix.CreateScale(Zoom) 
        }
        /// <summary>
        /// Update camera.
        /// </summary>
        public void Update()
        {
            if (Follow)
                FollowPlayer();
            else
            {
                Bounds = Viewport.Bounds;
                UpdateMatrix();
                SetCameraMovement();                
            }
        }
        /// <summary>
        /// Set the camera movement based on user input.
        /// </summary>
        private void SetCameraMovement()
        {
            Vector2 cameraMovement = Vector2.Zero;
            int moveSpeed = SetCameraSpeed();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                cameraMovement.Y = -moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                cameraMovement.Y = moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                cameraMovement.X = -moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                cameraMovement.X = moveSpeed;

            MoveCamera(cameraMovement);
        }

        /// <summary>
        /// Set camera speed based on the zoom.
        /// </summary>
        /// <returns></returns>
        private int SetCameraSpeed()
        {
            int moveSpeed;

            if (Zoom > .8f)
                moveSpeed = 15;            
            else if (Zoom < .8f && Zoom >= .6f)
                moveSpeed = 20;           
            else if (Zoom < .6f && Zoom > .35f)
                moveSpeed = 25;            
            else if (Zoom <= .35f)
                moveSpeed = 30;            
            else
                moveSpeed = 10;

            AdjustZoom();

            return moveSpeed;
        }
    }
}
