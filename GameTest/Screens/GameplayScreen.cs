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
        Monster monster;
        Map map;
     
        /// <summary>
        /// load content from file.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            XmlManager<Player> playerLoader = new XmlManager<Player>();
            XmlManager<Map> mapLoader = new XmlManager<Map>();
            XmlManager<Monster> monsterLoader = new XmlManager<Monster>();
            player = playerLoader.Load("Load/Gameplay/Player.xml");
            map = mapLoader.Load("Load/Gameplay/Maps/Map1.xml");
            monster = monsterLoader.Load("Load/Gameplay/Monster1.xml");
            player.LoadContent();
            map.LoadContent();
            monster.LoadContent();

            Camera.Instance.Player = player;
            Camera.Instance.Follow = true;
            ScreenManager.Instance.CurrentMap = map;
            Collision.Instance.player = player;
        }
        /// <summary>
        /// unload content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();
            monster.UnloadContent();
        }
        /// <summary>
        /// update objects.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            player.Update(gameTime);
            map.Update(gameTime);//, player);
            monster.Update(gameTime);
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
            monster.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
