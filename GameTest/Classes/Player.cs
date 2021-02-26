using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest
{
    public class Player : Actor
    {
        public int ExperienceRequired { get; set; }
        public int CurrentExperience { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }

        public Player()
        {
            Health = 521;
            AttackForce =350;
            Deffence = 180;
            Level = 1;
            ExperienceRequired = 500;
        }
    }
}
