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
    }

    // Update is called once per frame
    void Update()
    {
        totalmoneytext.GetComponent<Text>().text = PlayerStats.getMoney().ToString();
        totalbombstext.GetComponent<Text>().text = PlayerStats.getBombs().ToString();
    }
}
