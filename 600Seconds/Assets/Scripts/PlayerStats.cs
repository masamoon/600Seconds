using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static int money, bombs;
    private static int maxFuel;

    private static int lives;
    public enum Difficulty { EASY,MEDIUM,HARD};
    public enum Upgrade { None, Fuel, Bombs, DoubleGems, TripleGems, ExtraLife };

    private static Difficulty currentDifficulty;

    private static Upgrade currentUpgrade;

    private static bool chestOpened;

    private static float timeRemaining = 600f;

    private static int numRerouts = 0;

    private static bool hasDoubleGems, hasTripleGems;

    private static bool hasShovel, hasHookshot;

    private static bool exitPortalSpawned;

    static PlayerStats()
    {
        maxFuel = 100;
        numRerouts = 3;
        bombs = 3;
        lives = 1;
        hasDoubleGems = false;
        hasTripleGems = false;
        hasShovel = false;
        hasHookshot = false;
    }

    public static void resetGame()
    {
        money = 0;
        maxFuel = 100;
        numRerouts = 3;
        timeRemaining = 600f;
        bombs = 3;
        lives = 1;
        hasDoubleGems = false;
        hasTripleGems = false;
        hasShovel = false;
        hasHookshot = false;

    }


    public static float getTimeRemaining()
    {
        return timeRemaining;
    }

    public static void setTimeRemaining(float t)
    {
        timeRemaining = t;
    }

    public static int getMaxFuel()
    {
        return maxFuel;
    }

    public static void setMaxFuel(int f)
    {
        maxFuel = f;
    }
    public static int getMoney()
    {
        return money;
    }

    public static void setMoney(int m)
    {
        money = m;
    }

    public static int getBombs()
    {
        return bombs;
    }

    public static void setBombs(int b)
    {
        bombs = b;
    }

    public static Upgrade getCurrentUpgrade()
    {
        return currentUpgrade;
    }

    public static void setCurrentUpgrade(Upgrade u)
    {
        currentUpgrade = u;
    }

    public static Difficulty getCurrentDifficulty()
    {
        return currentDifficulty;
    }

    public static void setCurrentDifficulty(Difficulty d)
    {
        currentDifficulty = d;
    }

    public static bool getChestOpened()
    {
        return chestOpened;
    }

    public static void setChestOpened(bool b)
    {
        chestOpened = b;
    }

    public static int getNumRerouts()
    {
        return numRerouts;
    }

    public static void setNumRerouts(int r)
    {
        numRerouts = r;
    }

    public static bool getHasDoubleGems()
    {
        return hasDoubleGems;
    }

    public static void setHasDoubleGems(bool b)
    {
        hasDoubleGems = b;
    }

    public static bool getHasTripleGems()
    {
        return hasTripleGems;
    }

    public static void setHasTripleGems(bool b)
    {
        hasTripleGems = b;
    }

    public static void setHasShovel(bool b)
    {
        hasShovel = b;
    }

    public static bool getHasShovel()
    {
        return hasShovel;
    }

    public static void setHasHookshot(bool b)
    {
        hasHookshot = b;
    }

    public static bool getHasHookshot()
    {
        return hasHookshot;
    }

    public static bool getExitPortalSpawned()
    {
        return exitPortalSpawned;
    }

    public static void setExitPortalSpawned(bool b)
    {
        exitPortalSpawned = b;
    }










}
