using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float splashrange;
    // Start is called before the first frame update
    Rigidbody2D rb;
    SoundManager soundManager;

    Animator animator;
    void Start()
    {
        splashrange = 0.5f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] hitcolliders = Physics2D.OverlapCircleAll(gameObject.transform.position,splashrange);

        foreach(Collider2D hc in hitcolliders)
        {
            if (hc.gameObject.CompareTag("Breakable"))
            {
                animator.SetBool("isExploded", true);
                Destroy(hc.gameObject);
                rb.bodyType = RigidbodyType2D.Static;
                soundManager.Jump();
                Destroy(gameObject,0.5f);
                //animator.SetBool("isExploded", false);
            }
        }
    }
}
