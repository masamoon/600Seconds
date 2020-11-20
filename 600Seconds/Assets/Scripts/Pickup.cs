using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public int gemvalue;
    public Sprite[] gems;
    public int rarity;
    public bool catchable;
    public SoundManager soundManager;
    int gemMultiplier;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerStats.getHasDoubleGems() )
        {
            gemMultiplier = 2;
        }
        else if (PlayerStats.getHasTripleGems())
        {
            gemMultiplier = 3;
        }
        else
        {
            gemMultiplier = 1;
        }

        soundManager = GetComponent<SoundManager>();

        if(PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.EASY)
        {
            rarity = 1;
        }
        else if (PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.MEDIUM)
        {
            rarity = 3;
        }
        else if (PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.HARD)
        {
            rarity = 4;
        }
        
        //catchable = true;
        float randgem = Random.value;
        //if (catchable) //check if gem is thrown or spawned on the level
       // {
            if (randgem < (0.10 * rarity))
            {
                gemvalue = 1000;
                gameObject.GetComponent<SpriteRenderer>().sprite = gems[2];
            }
            else if (randgem < (0.20 * rarity))
            {
                gemvalue = 500;
                gameObject.GetComponent<SpriteRenderer>().sprite = gems[1];
            }
            else
            {
                gemvalue = 100;
                gameObject.GetComponent<SpriteRenderer>().sprite = gems[0];
            }

        rb = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //CheckIfOnAir();
    }

    void CheckIfOnAir()
    {
        if(rb.velocity != Vector2.zero)
        {
            Physics.IgnoreLayerCollision(10, 11);
        }
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("colision with gem");
        if(col.gameObject.CompareTag("Player") && catchable){
            //Debug.Log("picked up gem");
            soundManager.Jump();
            Score.score += gemvalue*gemMultiplier;
           
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            catchable = false;
            Destroy(gameObject,0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log("colision with gem");
        if (col.gameObject.CompareTag("Player") && catchable)
        {
            //Debug.Log("picked up gem");
            soundManager.Jump();
            Score.score += gemvalue*gemMultiplier;

            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            catchable = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
