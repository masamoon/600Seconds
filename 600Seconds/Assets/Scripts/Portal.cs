using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public GameObject keysprite;
    private enum State { RANGE,NOTRANGE}
    private State currentState;
    Renderer keyspriteimg;
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        keyspriteimg = keysprite.GetComponent<Renderer>();
        keyspriteimg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.RANGE)
        {
            TeleportOut();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keyspriteimg.enabled = true;
            currentState = State.RANGE;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            keyspriteimg.enabled = true;
            currentState = State.RANGE;
        }

    }

    public void TeleportOut()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Take Portal");
            keyspriteimg.enabled = false;
            int wealth = PlayerStats.getMoney() + Score.score;
            PlayerStats.setMoney(wealth);
            GameObject timer_gobj = GameObject.FindGameObjectWithTag("Timer");
            timer = timer_gobj.GetComponent<Timer>();
            PlayerStats.setTimeRemaining(timer.timeRemaining);
            if (PlayerStats.getChestOpened())
            {
                spawnUpgrade();
            }
            SceneManager.LoadScene("Spaceship");
            

        }
    }

    public void spawnUpgrade()
    {
        if(PlayerStats.getCurrentUpgrade() == PlayerStats.Upgrade.Bombs)
        {
            int bombs = PlayerStats.getBombs();
            PlayerStats.setBombs(bombs + 3);
        }
        else if(PlayerStats.getCurrentUpgrade() == PlayerStats.Upgrade.Fuel)
        {
            PlayerStats.setMaxFuel(PlayerStats.getMaxFuel() + 50);
        }
        else if(PlayerStats.getCurrentUpgrade() == PlayerStats.Upgrade.DoubleGems)
        {
            PlayerStats.setHasDoubleGems(true);
        }
        else if (PlayerStats.getCurrentUpgrade() == PlayerStats.Upgrade.TripleGems)
        {
            PlayerStats.setHasTripleGems(true);
        }

        print("New Upgrade: " + PlayerStats.getCurrentUpgrade().ToString());
        print("Bombs: " + PlayerStats.getBombs().ToString());
        print("MaxFuel: " + PlayerStats.getMaxFuel().ToString());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        keyspriteimg.enabled = false;
        currentState = State.NOTRANGE;
        // Debug.Log("Leave Portal");


    }
}
