using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4thVision.Gbanga.RPG.Logic.Classes
{
    public class Enemy
    {
        private int myXP;
        public Enemy(int xp)
        {
            myXP = xp;
        }

        public int die()
        {
            return myXP;
        }
    }
}
