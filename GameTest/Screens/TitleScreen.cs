using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Game title and menu screen.
    /// </summary>
    public class TitleScreen : GameScreen
    {
        MenuManager menuManager;
        /// <summary>
        /// initialize.
        /// </summary>
        public TitleScreen()
        {
            menuManager = new MenuManager();
        }
        /// <summary>
        /// Load Content from file.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            menuManager.LoadContent("Load/TitleMenu.xml");
        }
        /// <summary>
        /// Unload content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            menuManager.UnloadContent();
        }
        /// <summary>
        /// Update screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            menuManager.Update(gameTime);
        }
        /// <summary>
        /// Draw content on screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
