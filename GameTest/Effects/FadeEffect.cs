using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Fade effect that inherits from image effect.
    /// </summary>
    public class FadeEffect : ImageEffect
    {
        public float FadeSpeed;
        public bool Increase;
        public bool IsMonster;
        /// <summary>
        /// Initialization.
        /// </summary>
        public FadeEffect()
        {
            FadeSpeed = 1;
            Increase = false;
        }

        /// <summary>
        /// Load image.
        /// </summary>
        /// <param name="Image">Given image</param>
        public override void LoadContent(Image Image)
        {
            base.LoadContent(Image);
        }

        /// <summary>
        /// unload any content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update the effect.
        /// The image opacity fluctuates between 1 and 0.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.IsActive)
            {
                if (!Increase)
                    image.Alpha -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.Alpha += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                
                if (!IsMonster && image.Alpha < 0.0f)
                {
                    Increase = true;
                    image.Alpha = 0.0f;
                }
                else if (!IsMonster && image.Alpha > 1.0f)
                {
                    Increase = false;
                    image.Alpha = 1.0f;
                }
                else if(IsMonster)
                {
                    Increase = false;
                }
                
                if(IsMonster && image.Alpha == 0.0f)
                {
                    UnloadContent();
                }
            }
            else
                image.Alpha = 1.0f;
        }
    }
}
