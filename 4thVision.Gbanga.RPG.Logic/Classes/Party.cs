using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4thVision.Gbanga.RPG.Logic.Classes
{
    public class Party
    {

        

        private List<Player> myMembers;

        #region "constructor"

        public Party()
        {
            myMembers = new List<Player>();

        }

        #endregion

        #region "public properties"
        public List<Player> getPlayers
        {
            get
            {
                return myMembers;
            }
        }

        #endregion

        #region "public functions"

        public void addPlayer(Player p)
        {
            try
            {
                if (!myMembers.Contains(p)) myMembers.Add(p);
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("Error addPLayer {0}", ex.Message));
            }                
        }
        public void removePlayer(Player p)
        {
            try
            {
                if (myMembers.Contains(p)) myMembers.Remove(p);
            }
            catch(Exception ex)
            {
                Debug.Print(String.Format("Error removePlayer {0}", ex.Message));
            }
        }

        public void clearPlayers()
        {
            try
            {
                myMembers.Clear();
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("Error clearPlayers {0}", ex.Message));
            }
        }

        public void distributeXP(long xp)
        {
            try
            {
                long dividedXP = xp / myMembers.Count;
                foreach (Player p in myMembers) p.addExperience(dividedXP);

            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("Error distributeXP {0}", ex.Message));
            }
        }

        #endregion
    }
}