using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Actor
    {
        protected Image CurrentState { get; set; }
        protected Image StateAttack { get; set; }
        protected Image StateIdle { get; set; }
        protected Vector2 Velocity { get; set; }
        protected float MoveSpeed { get; set; }
        protected double Attack { get; set; }
        protected double Deffence { get; set; }
        protected double Health { get; set; }
        protected double AttackSpeed { get; set; }
        protected int Level { get; set; }
        protected bool IsAlive { get; set; }
        protected bool ISEngaged { get; set; }
        protected Vector2 Target;
        protected Vector2 Origin;

        public Actor()
        {
            Origin = Vector2.Zero;
            Target = Origin;
        }
        public void SendAttack(Image target)
        {
            //change image, start effect
            if(target.Position.X < CurrentState.Position.X)
            {}
            else if (target.Position.X > CurrentState.Position.X)
            { }
            else if (target.Position.Y < CurrentState.Position.Y)
            { }
            else if (target.Position.X > CurrentState.Position.Y)
            { }
        }
        public void LoadContent()
        {
            CurrentState.LoadContent();
        }
        public void UnloadContent()
        {
            CurrentState.UnloadContent();
        }
        public void Update(GameTime gameTime)
        {
            if (!IsAlive)
            { }
            else if (ISEngaged)
            { }//change image
            else 
            { }//change to idle
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentState.Draw(spriteBatch);
        }
    }
}
