using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameTest
{
    /// <summary>
    /// This Screen contains the game objects.
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        Player player;
        Map map;
     
        /// <summary>
        /// load content from file.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            XmlManager<Player> playerLoader = new XmlManager<Player>();
            XmlManager<Map> mapLoader = new XmlManager<Map>();
            player = playerLoader.Load("Load/Gameplay/Player.xml");
            map = mapLoader.Load("Load/Gameplay/Maps/Map1.xml");
            //map = mapLoader.Load("Load/Gameplay/Maps/GridMap.xml");
            player.LoadContent();
            map.LoadContent();

            Camera.Instance.Player = player;
            Camera.Instance.Follow = true;
            ScreenManager.Instance.CurrentMap = map;
        }
        /// <summary>
        /// unload content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
        }
        /// <summary>
        /// update objects.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime, player);            
        }

        /// <summary>
        /// Draw objects on screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch, "Underlay");
            player.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
