using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// Manage menu content.
    /// </summary>
    public class MenuManager
    {
        Menu menu;
        bool IsTransitioning;
        /// <summary>
        /// Transition effect for each menu item.
        /// </summary>
        /// <param name="gameTime"></param>
        void Transition(GameTime gameTime)
        {
            if(IsTransitioning)
            {                
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    menu.Items[i].Image.Update(gameTime);
                    float first = menu.Items[0].Image.Alpha;
                    float last = menu.Items[menu.Items.Count - 1].Image.Alpha;

                    if (first == 0.0f && last == 0.0f)
                    {
                        menu.ID = menu.Items[menu.ItemNumber].LinkID;
                    }
                    else if (first == 1.0f && last == 1.0f)
                    { 
                        IsTransitioning = false;
                        foreach (MenuItem item in menu.Items)
                            item.Image.RestoreEffects();
                    }
                }
            }
        }
        /// <summary>
        /// initialize.
        /// </summary>
        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += Menu_OnMenuChange;
        }
        /// <summary>
        /// Chnge screen based on the menu choice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_OnMenuChange(object sender, EventArgs e)
        {
            XmlManager<Menu> xmlManager = new XmlManager<Menu>();
            menu.UnloadContent();
            menu = xmlManager.Load(menu.ID);
            menu.LoadContent();
            menu.OnMenuChange += Menu_OnMenuChange;
            menu.Transition(0.0f);

            foreach (MenuItem item in menu.Items)
            {
                item.Image.StoreEffects();
                item.Image.ActivateEffect("FadeEffect");
            }
        }
        /// <summary>
        /// Load content.
        /// </summary>
        /// <param name="menuPath"></param>
        public void LoadContent(string menuPath)
        {
            if (menuPath != String.Empty)
                menu.ID = menuPath;
        }
        /// <summary>
        /// unlaod content.
        /// </summary>
        public void UnloadContent()
        {
            menu.UnloadContent();
        }
        /// <summary>
        /// update Menu and screen and change screen if that option is selected in the menu.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if(!IsTransitioning)
                menu.Update(gameTime);

            if(InputManager.Instance.KeyPressed(Keys.Enter) && !IsTransitioning)
            {

                if (menu.Items[menu.ItemNumber].LinkType == "Screen")
                {
                    ScreenManager.Instance.ChangeScreens(menu.Items[menu.ItemNumber].LinkID);
                }
                else
                {
                    IsTransitioning = true;
                    menu.Transition(1.0f);
                    foreach(MenuItem item in menu.Items)
                    {
                        item.Image.StoreEffects();
                        item.Image.ActivateEffect("FadeEffect");
                    }
                }
            }
            Transition(gameTime);
        }
        /// <summary>
        /// draw content.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
