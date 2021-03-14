using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace GameTest
{
    /// <summary>
    /// A game screen or game scene.
    /// </summary>
    public class GameScreen
    {
        protected ContentManager content;
        [XmlIgnore]
        public Type Type;

        public string XmlPath;
        /// <summary>
        /// initialize and load a screen.
        /// </summary>
        public GameScreen()
        {
            Type = this.GetType();
            XmlPath = "Load/" + Type.ToString().Replace("GameTest.", "") + ".xml";
        }
        /// <summary>
        /// load content.
        /// </summary>
        public virtual void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");
        }
        /// <summary>
        /// unload content.
        /// </summary>
        public virtual void UnloadContent()
        {
            content.Unload();
        }
        /// <summary>
        /// update content.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();

        }
        /// <summary>
        /// draw content
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {}
    }
}
