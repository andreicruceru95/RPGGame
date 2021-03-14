using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Effect for atlas-type images.
    /// This class splits an image in a 2d array and reffer to each of the frames.
    /// </summary>
    public class SpriteSheetEffect : ImageEffect
    {
        public int FrameCounter;
        public int SwitchFrame;
        public Vector2 CurrentFrame;
        public Vector2 AmountOfFrames;
        /// <summary>
        /// return frame width.
        /// </summary>
        public int FrameWidth
        {
            get 
            {
                if (image.Texture != null)
                    return image.Texture.Width / (int)AmountOfFrames.X;
                return 0;
            }
        }
        /// <summary>
        /// return frame height.
        /// </summary>
        public int FrameHeight
        {
            get
            {
                if (image.Texture != null)
                    return image.Texture.Height / (int)AmountOfFrames.Y;
                return 0;
            }
        }
        /// <summary>
        /// initialize.
        /// </summary>
        public SpriteSheetEffect()
        {
            AmountOfFrames = new Vector2(3, 4);
            CurrentFrame = new Vector2(1, 0);
            SwitchFrame = 100;
            FrameCounter = 0;
        }
        /// <summary>
        /// load image content.
        /// </summary>
        /// <param name="Image"></param>
        public override void LoadContent(Image Image)
        {
            base.LoadContent(Image);
        }
        /// <summary>
        /// unload content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// if image is active, we are switching frames
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.IsActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;

                    if (CurrentFrame.X * FrameWidth >= image.Texture.Width)
                        CurrentFrame.X = 0;
                }
            }
            else
                CurrentFrame.X = 1;

            image.SourceRect = new Rectangle((int)CurrentFrame.X * FrameWidth,
                (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
        }
    }
}
