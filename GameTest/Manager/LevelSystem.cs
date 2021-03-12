using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest.Manager
{
    public class LevelSystem
    {
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;

        private static LevelSystem Instance;
        public  int Level;
        public int Experience;
        public int ExperienceToNextLevel;

        public LevelSystem()
        {
            Instance = this;
            Level = 0;
            Experience = 0;
            ExperienceToNextLevel = 100;
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
            if(Experience >= ExperienceToNextLevel)
            {
                Level++;
                Experience -= ExperienceToNextLevel;

                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }

            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
        //levelSystem_OnExperienceChanged(object sender, System.EventArgs e){SetLevelNumber(levelSystem.GetLevelNumber())}
        //same with exp bar

        public float GetExperienceNormalized()
        {
            return Experience / ExperienceToNextLevel;
        }
    }
}
