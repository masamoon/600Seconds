using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dashboard : MonoBehaviour
{

    public GameObject totalmoneytext;
    public GameObject totalbombstext;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStats.getMoney() >= 100000)
        {
            SceneManager.LoadScene("Victory");
        }

        PlayerStats.setChestOpened(false);
    }

    // Update is called once per frame
    void Update()
    {
        totalmoneytext.GetComponent<Text>().text = PlayerStats.getMoney().ToString();
        totalbombstext.GetComponent<Text>().text = PlayerStats.getBombs().ToString();

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddMoney(50000);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddMoney(PlayerStats.getMoney()+50000);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStats.setHasDoubleGems(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerStats.setNumRerouts(50);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerStats.setHasTripleGems(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerStats.setBombs(50);
        }


    }

    void AddMoney(int money)
    {
        PlayerStats.setMoney(money);
    }
}
