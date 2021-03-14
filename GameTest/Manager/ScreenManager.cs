using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace GameTest
{
    /// <summary>
    /// Screen manager is responsible with managing screen content.
    /// </summary>
    public class ScreenManager
    {
        private static ScreenManager instance;
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        XmlManager<GameScreen> xmlGameScreenManager;

        GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;
        [XmlIgnore]
        public SpriteFont Font { get; private set; }

        public Image Image;
        [XmlIgnore]
        public static int ScreenWidth { get; private set; } = 1280;
        [XmlIgnore]
        public static int ScreenHeight { get; private set; } = 720;
        [XmlIgnore]
        public static Viewport viewport = new Viewport(0, 0, ScreenWidth, ScreenHeight);

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }
        [XmlIgnore]
        public Map CurrentMap;
       
        /// <summary>
        /// force to singleton.
        /// </summary>
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }

                return instance;
            }
        }
        /// <summary>
        /// Change the screen.
        /// </summary>
        /// <param name="screenName"></param>
        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("GameTest." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }
        /// <summary>
        /// Manage screen transition.
        /// </summary>
        /// <param name="gameTime"></param>
        void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlGameScreenManager.Type = currentScreen.Type;
                    if (File.Exists(currentScreen.XmlPath))
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
                    currentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }       

        /// <summary>
        /// Initialize
        /// </summary>
        public ScreenManager()
        {
            Camera.Instance.Viewport = viewport;
            Dimensions = new Vector2(ScreenWidth, ScreenHeight);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");            
        }
        /// <summary>
        /// Load content
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            Image.LoadContent();
            Font = Content.Load<SpriteFont>("Arial");
        }
        /// <summary>
        /// Unload content
        /// </summary>
        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }
        /// <summary>
        /// Update screen
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Camera.Instance.Update();
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }
        /// <summary>
        /// Draw on screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }
    }
}
