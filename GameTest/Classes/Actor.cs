using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Actor
    {
        public string Name { get; set; }
        
        public int Health { get; set; }
        public int AttackForce { get; set; }
        public int Deffence { get; set; }
        public int Level { get; set; }
        //public int ExperienceRequired { get; set; }
        //public int CurrentExperience { get; set; }
        //public int Gold { get; set; }

        public Actor()
        {            
        }
    }
}
