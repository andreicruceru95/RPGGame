using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Splash screen is the first scren the user will see as he plays the game.
    /// </summary>
    public class SplashScreen : GameScreen
    {
        public Image Image;
        /// <summary>
        /// load content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
        }
        /// <summary>
        /// unload content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }
        /// <summary>
        /// update content.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            //load the title screen when enter or z keys are pressed.
            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }
        /// <summary>
        /// Draw content on screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
