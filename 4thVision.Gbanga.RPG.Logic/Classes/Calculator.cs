using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4thVision.Gbanga.RPG.Logic.Classes
{
    public static class Calculator
    {

            // formula = XP = A + B + C;    
            // A = coeff * numerator / (lvl^pow)    // i.e. 100 * 2 / (level^2)
            // B = coeff * lvl^pow / denom          // i.e. 100 * level^2 / 2
            // C = coeff * numerator / denom        // fixvalue


        #region "formula"

        public static float coeff_FormulaA = 0;
        public static float numerator_FormulaA = 0;
        public static float pow_lvlDenom_FormulaA = 0;
                       
        public static float coeff_FormulaB = 0;
        public static float pow_lvlNumerator_FormulaB = 0;
        public static float denom_FormulaB = 0;
                       
        public static float coeff_FormulaC = 0;
        public static float numerator_FormulaC = 0;
        public static float denom_FormulaC = 0;

        #endregion

        #region "static"

        public static int maxLevel = 100;

        #endregion


        #region "public functions"

        public static long calcXP(long lvl)
        {
            try
            {
                if (lvl > 100) lvl = 100;

                long myA = (Math.Pow(lvl, pow_lvlDenom_FormulaA) != 0) ? (long) (coeff_FormulaA * numerator_FormulaA / Math.Pow(lvl, pow_lvlDenom_FormulaA)): 0;
                long myB = (denom_FormulaB !=0 ) ? (long) (coeff_FormulaB * Math.Pow(lvl,pow_lvlNumerator_FormulaB) / denom_FormulaB) : 0;
                long myC = (denom_FormulaC != 0) ? (long)(coeff_FormulaC * numerator_FormulaC / denom_FormulaC) : 0;
                return myA + myB + myC;
            }
            catch (Exception ex)
            {
                Debug.Print(string.Format("error calcXP {0}", ex.Message));
                return -1;
            }
        }
        public static long getMissingXPforNextLevel(long xp, long lvl)
        {

            try
            {
                long r = 0;

                long levelUpXP = calcXP(lvl + 1);
                r = levelUpXP - xp;
                return r;
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error getMissingXPforNextxLevel {0}", ex.Message));
                return 0;
            }

        }

        public static long getXPforLevel(long lvl)
        {
            try
            {
                long r = calcXP(lvl);
                return r;
            }
            catch(Exception ex)
            {
                Debug.Print(String.Format("error getXPforLevel {0}", ex.Message));
                return -1;
               
            }
        }

        public static long getLvlFromXP(long xp)
        {

             try
            {
                long r = 0;
                if (xp < 0) return r;
                if(coeff_FormulaA == 0 && coeff_FormulaB != 0)
                {
                    if((denom_FormulaB*(coeff_FormulaC*numerator_FormulaC - xp * denom_FormulaC)/(coeff_FormulaB*denom_FormulaC)) <0)
                    {
                        r = (long)Math.Pow(((-denom_FormulaB * (coeff_FormulaC * numerator_FormulaC - xp * denom_FormulaC)) /
                                                                            (coeff_FormulaB * denom_FormulaC)),
                                                                    (1 / pow_lvlNumerator_FormulaB));

                    }
                    else
                    {
                        return 0;
                    }

                }
                else if(coeff_FormulaB == 0 && coeff_FormulaA !=0)
                {
                    if(((coeff_FormulaA*numerator_FormulaA*denom_FormulaC)/(coeff_FormulaC*numerator_FormulaC-xp*denom_FormulaC)) <0)
                    {
                        r = (long)Math.Pow(((-coeff_FormulaA * numerator_FormulaA * denom_FormulaC) / (coeff_FormulaC * numerator_FormulaC - xp * denom_FormulaC)), (1 / pow_lvlDenom_FormulaA));
                    }
                    else
                    {
                        return 0;
                    }
                    
                }
                else if(coeff_FormulaA == 0 && coeff_FormulaB== 0)

                {
                    //doesnt matter right now if there is no LEVEL in formula C
                    return 0;
                }
                return r;
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error getLvlFromXp {0}", ex.Message));
                return -1;
            }
        }

        public static double getPercentualXPcurrentLevel(long xp)
        {
            try
            {
                double r = 0;
                long xpStartLvl =  getXPforLevel((int)getLvlFromXP(xp));
                long xpNextlvl = getXPforLevel((int)getLvlFromXP(xp)+1);

                long normalizedCurrXP = xp - xpStartLvl;
                long normalizedEndXP = xpNextlvl - xpStartLvl;

                r = ((double)normalizedCurrXP / (double)normalizedEndXP) *100;

                return r;
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error getPercentualXPcurrentLevel {0}", ex.Message));
                return -1;
            }
        }

        public static long getDeltaXPtoLevel(long lvl, long xp)
        {
            try
            {
                long r;
                long targetXP = getXPforLevel(lvl);
                r = targetXP - xp;

                return r;
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error getDeltaXPtoLevel {0}", ex.Message));
                return 0;
            }
        }

        public static long getDeltaXPtoNextLevel(long xp)
        {
            try
            {
                long currLvl = getLvlFromXP(xp);
                long delta = getXPforLevel(currLvl + 1);
                delta -= xp;
                return delta;
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error getDeltaXPtoNextLevel {0}", ex.Message));
                return 0;
            }
        }

        public static long getXPtoNextLevel(long xp)
        {
            try
            {
                long currLevel = getLvlFromXP(xp);
                long nextLevel = currLevel + 1;
                long missingXP = getXPforLevel(nextLevel) - xp;
                return missingXP;
            }
            catch (Exception ex)
            {
                Debug.Print(String.Format("error getXPtoNextLevel {0}", ex.Message));
                return 0;
            }
        }

        #endregion
    }
}
