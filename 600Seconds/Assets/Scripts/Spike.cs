using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    SoundManager soundManager;
    
    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    
    void Update()
    {
        
    }

   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerDeath(col);
        }
    }

    private void PlayerDeath(Collider2D col)
    {
        Animator animator = col.gameObject.GetComponent<Animator>();
       // col.gameObject.GetComponent<platformer>().enabled = false;
        animator.SetBool("isJumping", false);
        if (!animator.GetBool("isDead"))
        {
            soundManager.Jump();
            animator.SetBool("isDead", true);

        }
    }
}
