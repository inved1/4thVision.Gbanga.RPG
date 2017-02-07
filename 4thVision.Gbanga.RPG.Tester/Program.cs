using _4thVision.Gbanga.RPG.Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4thVision.Gbanga.RPG.Tester
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("RPG Logic Tester");
            Console.WriteLine("Case1: Party of four, Startlevel 0, 10 Enemys, each 150 XP");
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();

            Case1();

            Console.WriteLine("---------------");
            Console.WriteLine("- CASE 1 DONE -");
            Console.WriteLine("---------------");
            Console.WriteLine("Press Enter for CASE 2");
            Console.ReadLine();

            Console.WriteLine("Case2: same Party, set Players to Level 11, 10 Enemys, each 4000 XP");
         

            Case2();

            

            Console.ReadLine();


        }

        private static void  Case1() 
        {

            Console.WriteLine("XP Formula: XP = 100 * (lvl^2)/2");
            List<Enemy> badGuys = new List<Enemy>();
            Party theParty = new Party();

            Calculator.coeff_FormulaA = 0;
            Calculator.numerator_FormulaA = 1;
            Calculator.pow_lvlDenom_FormulaA = 1;

            Calculator.coeff_FormulaB = 100;
            Calculator.denom_FormulaB = 2;
            Calculator.pow_lvlNumerator_FormulaB = 2;

            Calculator.coeff_FormulaC = 0;
            Calculator.denom_FormulaC = 1;
            Calculator.numerator_FormulaC = 0;


            for (int i = 0; i < 4; i++) theParty.addPlayer(new Player(string.Format("Player {0}", i+1)));

            for (int i = 0; i < 10; i++) badGuys.Add(new Enemy(150));


            int enemysAlive = badGuys.Count;
            while (enemysAlive > 0)
            {
                Enemy currEnemy = badGuys.First();
                int xpToDistribute = currEnemy.die();
                Console.WriteLine(string.Format("Enemy dies, distributes {0} XP among players", xpToDistribute));

                theParty.distributeXP(xpToDistribute);

                foreach (Player p in theParty.getPlayers) Console.WriteLine("Player [{0}]", p.ToString());

                Console.WriteLine("------");
                Console.WriteLine("");
                enemysAlive -= 1;
            }

            Console.WriteLine("some Infos:");
            Console.WriteLine("Player 1 needs {0} XP for next Level (Currently on {1}/{2} (xp/lvl))",
                Calculator.getMissingXPforNextLevel(theParty.getPlayers[0].getExperience,
                    theParty.getPlayers[0].getLevel),
                theParty.getPlayers[0].getExperience,
                theParty.getPlayers[0].getLevel);

            Console.WriteLine("Player 2 Percentage of current Level: {0} (CurrentLevel {1} ({2}) --- Current XP {3} --- Next Level {4} ({5})", 
                Calculator.getPercentualXPcurrentLevel(theParty.getPlayers[1].getExperience),
                theParty.getPlayers[1].getLevel,
                Calculator.getXPforLevel(theParty.getPlayers[1].getLevel),
                theParty.getPlayers[1].getExperience,
                theParty.getPlayers[1].getLevel +1,
                Calculator.getXPforLevel(theParty.getPlayers[1].getLevel +1));

            Console.WriteLine("Player 3 needs {0} XP for next Level", Calculator.getDeltaXPtoNextLevel(theParty.getPlayers[2].getExperience));
            Console.WriteLine("Player 3 needs {0} XP to Level {1}",Calculator.getDeltaXPtoLevel(5,theParty.getPlayers[2].getExperience),5);
            Console.WriteLine("Player 3 needs {0} XP to Level {1}", Calculator.getDeltaXPtoLevel(10, theParty.getPlayers[2].getExperience), 10);
            Console.WriteLine("Player 3 needs {0} XP to Level {1}", Calculator.getDeltaXPtoLevel(20, theParty.getPlayers[2].getExperience), 20);


            Console.WriteLine(String.Format("XP needed for level {0} : {1}", 1, Calculator.calcXP(1)));
            Console.WriteLine(String.Format("XP needed for level {0} : {1}", 2, Calculator.calcXP(2)));
            Console.WriteLine(String.Format("XP needed for level {0} : {1}", 5, Calculator.calcXP(5)));
            Console.WriteLine(String.Format("XP needed for level {0} : {1}", 10, Calculator.calcXP(10)));
            Console.WriteLine(String.Format("XP needed for level {0} : {1}", 50, Calculator.calcXP(50)));

            Console.WriteLine(String.Format("level if XP is  {0} : {1}", 100, Calculator.getLvlFromXP(100)));
            Console.WriteLine(String.Format("level if XP is  {0} : {1}", 200, Calculator.getLvlFromXP(200)));
            Console.WriteLine(String.Format("level if XP is  {0} : {1}", 500, Calculator.getLvlFromXP(500)));
            Console.WriteLine(String.Format("level if XP is  {0} : {1}", 1000, Calculator.getLvlFromXP(1000)));
            Console.WriteLine(String.Format("level if XP is  {0} : {1}", 50000, Calculator.getLvlFromXP(5000)));



        }

        private static void Case2()
        {

            Console.WriteLine("XP Formula: XP = 2 * (lvl^3)/2 + 500");
            List<Enemy> badGuys = new List<Enemy>();
            Party theParty = new Party();

            Calculator.coeff_FormulaA = 0;
            Calculator.numerator_FormulaA = 1;
            Calculator.pow_lvlDenom_FormulaA = 1;

            Calculator.coeff_FormulaB = 2;
            Calculator.denom_FormulaB = 2;
            Calculator.pow_lvlNumerator_FormulaB = 3;

            Calculator.coeff_FormulaC = 500;
            Calculator.denom_FormulaC = 1;
            Calculator.numerator_FormulaC = 1;



            for (int i = 0; i < 4; i++)
            {
                Player p = new Player(string.Format("Player {0}", i + 1));
                p.setLevel = 11;
                theParty.addPlayer(p);
            }

            for (int i = 0; i < 10; i++) badGuys.Add(new Enemy(4000));
            int enemysAlive = badGuys.Count;
            while (enemysAlive > 0)
            {
                Enemy currEnemy = badGuys.First();
                int xpToDistribute = currEnemy.die();
                Console.WriteLine(string.Format("Enemy dies, distributes {0} XP among players", xpToDistribute));

                theParty.distributeXP(xpToDistribute);

                foreach (Player p in theParty.getPlayers) Console.WriteLine("Player [{0}]", p.ToString());

                Console.WriteLine("------");
                Console.WriteLine("");
                enemysAlive -= 1;
            }

            Console.WriteLine("Some Functions");
            Console.WriteLine("reset Player 1");
            theParty.getPlayers[0].reset();
            Console.WriteLine("Player1 xp {0} , lvl {1}", theParty.getPlayers[0].getExperience, theParty.getPlayers[0].getLevel);

            Console.WriteLine("Add 5000 XP to Player 2, get info, subtract 6000 XP, get Info again");
            Console.WriteLine("Player2 currentState {0}", theParty.getPlayers[1].ToString());
            theParty.getPlayers[1].addExperience(5000);
            Console.WriteLine("-Player2 currentState {0}", theParty.getPlayers[1].ToString());
            theParty.getPlayers[1].addExperience(-6000);
            Console.WriteLine("-Player2 currentState {0}", theParty.getPlayers[1].ToString());


            Console.WriteLine("----");
            Console.WriteLine("Player3 get delta between current XP and level 30: Player3Info [{0}], Delta {1}",
                theParty.getPlayers[2].ToString(),
                Calculator.getDeltaXPtoLevel(30, theParty.getPlayers[2].getExperience));
            Console.WriteLine("Player3 reset XP to last LevelUP");
            Console.WriteLine("- {0}", theParty.getPlayers[2].ToString());
            theParty.getPlayers[2].resetXPtoLastLevel();
            Console.WriteLine("- {0}", theParty.getPlayers[2].ToString());
            Console.WriteLine("----");
            Console.WriteLine("Player 4 - add one Level twice, first [keepCurrentProgress=true], second [keepCurrentProgress=false]");
            Console.WriteLine("Player4 currentState {0}", theParty.getPlayers[3].ToString());
            theParty.getPlayers[3].addLevel(1, true);
            Console.WriteLine("-Player4 currentState {0}", theParty.getPlayers[3].ToString());
            theParty.getPlayers[3].addLevel(1, false);
            Console.WriteLine("-Player4 currentState {0}", theParty.getPlayers[3].ToString());




            Console.Read();

        }
    }
}
