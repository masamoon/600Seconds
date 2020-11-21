using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public Button hookshotbtn;
    public Button shovelbtn;

    void Start()
    {
        hookshotbtn.onClick.AddListener(() => BuyHookshot());
        shovelbtn.onClick.AddListener(() => BuyShovel());

        if (PlayerStats.getHasShovel())
        {
            shovelbtn.GetComponentInChildren<Text>().text = "Sold Out";
        }

        if (PlayerStats.getHasHookshot())
        {
            hookshotbtn.GetComponentInChildren<Text>().text = "Sold Out";
        }
    }

    public void BuyHookshot()
    {
        if (PlayerStats.getMoney() >= 10000)
        {
            hookshotbtn.GetComponentInChildren<Text>().text = "Sold Out";
            PlayerStats.setHasHookshot(true);
            PlayerStats.setMoney(PlayerStats.getMoney() - 10000);
        }
    }

    public void BuyShovel()
    {
        if (PlayerStats.getMoney() >= 5000)
        {
            shovelbtn.GetComponentInChildren<Text>().text = "Sold Out";
            PlayerStats.setHasShovel(true);
            PlayerStats.setMoney(PlayerStats.getMoney() - 5000);
        }
    }
    
}
