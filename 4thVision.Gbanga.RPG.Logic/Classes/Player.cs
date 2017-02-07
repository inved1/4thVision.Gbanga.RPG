using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4thVision.Gbanga.RPG.Logic.Classes
{
    public class Player
    {
        private long myExperience;
        private long myLevel;
        private string myName; 

        #region "properties"

        public long getLevel
        {
            get { return myLevel; }
        }
        public long getExperience
        {
            get { return myExperience; }
        }

        public string getName
        {
            get { return myName; }
        }

        public long setLevel
        {
            set {
                myLevel = value;
                //if it's set directly to a given level, set xp
                recalcExperience();
            }
        }

        public long setExperience
        {
            set
            {
                myExperience = value;
                recalcLevel();
            }
        }

        #endregion


        #region "public functions"


        public override string ToString()
        {
            return string.Format("Player [{0}] , XP: [{1}] , Level [{2}]", myName, myExperience, myLevel);
        }
        public void addLevel(int lvl, bool keepCurrentProgress)
        {
            try
            {
                long tmpXP = myExperience - Calculator.calcXP(myLevel);


                myLevel += lvl;
                recalcExperience();
                if (keepCurrentProgress)
                {
                    myExperience += tmpXP;
                    recalcLevel();
                }
            }
            catch(Exception ex)
            {
                Debug.Print(String.Format("error addLevel {0}", ex.Message));
            }

        }

        public void addExperience(long xp)
        {
            try
            {
                myExperience += xp;
                recalcLevel();
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error addExperience {0}", ex.Message));
            }
        }

        public void reset()
        {
            myExperience = 0;
            myLevel = 0;
        }

     
        public void resetXPtoLastLevel()
        {
            myExperience = Calculator.calcXP(myLevel);
        }

        #endregion

        #region "constructors"

        public Player(string name)
        {
            myName = name;
            myExperience = 0;
            myLevel = 0;
        }


        public Player(long ex, long lvl)
        {
            myExperience = ex;
            myLevel = lvl;
            //constructor could deliver "100 exp, level 312387123" which is just wrong
            recalcLevel();
        }

        #endregion


        #region "private functions"

        private void recalcLevel()
        {
            try
            {
                myLevel = Calculator.getLvlFromXP(myExperience);
               // myLevel = (long)Math.Sqrt((myExperience * 2f) / 100f);
            }
            catch(Exception ex)
            {
                Debug.Print(String.Format("Error recalcLevel: {0}",ex.Message));
            }
        }

        private void recalcExperience()
        {
            try
            {
                myExperience = Calculator.getXPforLevel(myLevel);
                //myExperience = (long)(100f * Math.Pow(myLevel, 2f) / 2f);
            }
            catch(Exception ex)
            {
                Debug.Print(String.Format("Error recalcExperience: {0}", ex.Message));
            }
        }


        #endregion

    }
}
