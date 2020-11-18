using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public enum Type { Rocky, Gas, Molten };
    //public enum Upgrade { None, Fuel, Bombs };

    public PlayerStats.Upgrade upgrade;

    public enum Difficulty { Easy, Medium, Hard };

    public Type planetType;
    public Difficulty planetDifficulty;
    public PlayerStats.Upgrade planetUpgrade;

    public Text planetType_text;
    public Text planetDifficulty_text;

    // Start is called before the first frame update
    void Start()
    {
        
        genDifficuty();
        genUpgrade();
        //genType();
        //genUpgrade();
    }

    // Update is called once per frame
    void Update()
    {
        planetDifficulty_text.text = planetDifficulty.ToString();
        planetType_text.text = getUpgradeText(planetUpgrade);
        
    }

    public string getUpgradeText(PlayerStats.Upgrade upgrade)
    {
        

        if(upgrade == PlayerStats.Upgrade.Bombs)
        {
            return "Bombs x3";
        }
        else if (upgrade == PlayerStats.Upgrade.Fuel)
        {
            return "Extra Fuel";
        }
        else if (upgrade == PlayerStats.Upgrade.DoubleGems)
        {
            return "Gems x2";
        }
        else if (upgrade == PlayerStats.Upgrade.TripleGems)
        {
            return "Gems x3";
        }
        else if (upgrade == PlayerStats.Upgrade.ExtraLife)
        {
            return "Extra Life";
        }
        else
        {
            return "None";
        }

            //return upgradeText;
    }

    public void genUpgrade()
    {
        float randUpgrade = Random.value;

        if(planetDifficulty == Difficulty.Easy)
        {
            planetUpgrade = PlayerStats.Upgrade.None;
        }
        else if (planetDifficulty == Difficulty.Medium)
        {

            if (randUpgrade < 0.8)
            {
                planetUpgrade = PlayerStats.Upgrade.Bombs;
            }
            else
            {

                float randNested = Random.value;
                if(randNested > 0.5)
                {
                    planetUpgrade = PlayerStats.Upgrade.Fuel;
                }
                else
                {
                    planetUpgrade = PlayerStats.Upgrade.DoubleGems;
                }
                
            }
           
        }
        else if (planetDifficulty == Difficulty.Hard)
        {
            if (randUpgrade < 0.8)
            {
                float randNested = Random.value;
                if (randNested > 0.5)
                {
                    planetUpgrade = PlayerStats.Upgrade.Fuel;
                }
                else
                {
                    planetUpgrade = PlayerStats.Upgrade.TripleGems;
                }
            }
            else
            {
                planetUpgrade = PlayerStats.Upgrade.Bombs;
            }
        }
    }

    public void genType()
    {
        float randtype = Random.value;

        if (randtype < 0.33)
        {
            planetType = Type.Gas;
        }
        else if (randtype < 0.66)
        {
            planetType = Type.Molten;
        }
        else
        {
            planetType = Type.Rocky;
        }
    }

    public void genDifficuty()
    {
        float randdiff = Random.value;

        if (randdiff < 0.4)
        {
            planetDifficulty = Difficulty.Easy;
        }
        else if (randdiff < 0.8)
        {
            planetDifficulty = Difficulty.Medium;
        }
        else
        {
            planetDifficulty = Difficulty.Hard;
        }

        
    }

    public void Reroll()
    {
        genDifficuty();
        genUpgrade();
        
        
    }
}
