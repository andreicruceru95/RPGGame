using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    /// <summary>
    /// This class will manage any input from the user.
    /// </summary>
    public class InputManager
    {
        KeyboardState currentKeyState, prevKeyState;
        MouseState mouse;

        private static InputManager instance;

        /// <summary>
        /// Force to singleton.
        /// </summary>
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }
        /// <summary>
        /// Update input manager.
        /// </summary>
        public void Update()
        {
            prevKeyState = currentKeyState;
            mouse = new MouseState();
            if (!ScreenManager.Instance.IsTransitioning)
                currentKeyState = Keyboard.GetState();
            mouse = Mouse.GetState();
        }
        /// <summary>
        /// Check if any keys are pressed, that weren't pressed previously.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Check if a key is released.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Check if a key is down.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Chech for mouse left click activity.
        /// </summary>
        /// <returns></returns>
        public bool IsLeftClick()
        {
            if (mouse.LeftButton == ButtonState.Pressed)
                return true;

            return false;
        }
    }
}
