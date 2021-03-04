﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class MenuManager
    {
        Menu menu;
        bool IsTransitioning;

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
        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += Menu_OnMenuChange;
        }

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

        public void LoadContent(string menuPath)
        {
            if (menuPath != String.Empty)
                menu.ID = menuPath;
        }
        public void UnloadContent()
        {
            menu.UnloadContent();
        }
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
        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}