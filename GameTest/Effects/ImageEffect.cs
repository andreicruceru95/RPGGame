using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Image effect.
    /// </summary>
    public class ImageEffect
    {
        protected Image image;
        public bool IsActive;
        /// <summary>
        /// initilize. 
        /// </summary>
        public ImageEffect()
        {
            IsActive = false;
        }

        /// <summary>
        /// Load image content.
        /// </summary>
        /// <param name="Image"></param>
        public virtual void LoadContent(Image Image)
        {
            this.image = Image;
        }
        /// <summary>
        /// unload content.
        /// </summary>
        public virtual void UnloadContent()
        {}
        /// <summary>
        /// Update game.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {}
    }
}
