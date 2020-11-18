﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public enum State {CLOSED,OPEN,GONE,RANGE};
    public State curState;
    public GameObject gem;
    public float invincibilityDurationSeconds;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        curState = State.CLOSED;
        transform.position += new Vector3(0, 0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (curState == State.OPEN)
        {
            
            
        }
        if (curState == State.GONE)
        {
            Destroy(gameObject);
        }
        if(curState == State.RANGE)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spawnLoot();
                curState = State.OPEN;
                PlayerStats.setChestOpened(true);
            }
        }

        //print(curState.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  print("in chest range");
        if (collision.gameObject.CompareTag("Player") && curState != State.OPEN)
            curState = State.RANGE;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      //  print("in chest range");
        if (collision.gameObject.CompareTag("Player") && curState != State.OPEN)
            curState = State.RANGE;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && curState != State.OPEN)
            curState = State.CLOSED;
    }

    void spawnLoot()
    {
        int numgems;

        if(PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.EASY)
        {
            numgems = (int)Random.Range(1, 3);
        }
        else if(PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.MEDIUM)
        {
            numgems = (int)Random.Range(3, 6);
        }
        else
        {
            numgems = (int)Random.Range(6, 15);
        }

        for(int i=0; i<numgems; i++)
        {
            GameObject throwgem = Instantiate(gem, gameObject.transform.position, Quaternion.identity);
            throwgem.GetComponent<Pickup>().catchable = false;
            Rigidbody2D throwgemrb2d = throwgem.GetComponent<Rigidbody2D>();
            throwgemrb2d.bodyType = RigidbodyType2D.Dynamic;
            throwgemrb2d.AddForce(new Vector2(2, 0) * 50);
            
            StartCoroutine(BecomeTemporarilyInvincible(throwgem));
            
            

        }

        PlayerStats.setNumRerouts(PlayerStats.getNumRerouts() + 1);
        PlayerStats.setBombs(PlayerStats.getBombs() + 1);

        Destroy(gameObject, 1f);

    }

    void MethodThatTriggersInvulnerability(GameObject throwgem)
    {
        if (throwgem.GetComponent<Pickup>().catchable)
        {
            StartCoroutine(BecomeTemporarilyInvincible(throwgem));
        }
    }


    private IEnumerator BecomeTemporarilyInvincible(GameObject throwgem)
    {
       // Debug.Log("Gem not catchable");
        throwgem.GetComponent<Pickup>().catchable = false;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

       // Debug.Log("Gem catchable");
        throwgem.GetComponent<Pickup>().catchable = true;

        //Destroy(gameObject);
        curState = State.GONE;
    }

    private void OnDestroy()
    {
        
    }



}