using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour {

    const string LEFT = "left";
    const string RIGHT = "right";

    const string UP = "up";
    const string DOWN = "down";

    public enum TypeMov {Horizontal,Vertical};

    SoundManager soundManager;

    Vector3 baseScale;
    
    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    [SerializeField]
    TypeMov typeMov;

    string facingDirection;

    Rigidbody2D rb2d;
    float moveSpeed = 2;

    bool isStuck;

    private void Start()
    {
        baseScale = transform.localScale;
        transform.position += new Vector3(0, 0.4f, 0);

        isStuck = false;

        if(typeMov == TypeMov.Horizontal)
        {
            facingDirection = RIGHT;
        }
        else
        {
            facingDirection = UP;
        }
        

        rb2d = GetComponent<Rigidbody2D>();

        soundManager = GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {

        float vX = moveSpeed;
        float vY = moveSpeed;

        if (!isStuck)
        {

            if (facingDirection == LEFT)
            {
                vX = -moveSpeed;
            }

            if (facingDirection == DOWN)
            {
                vY = -moveSpeed;
            }

            if (typeMov == TypeMov.Horizontal)
            {
                rb2d.velocity = new Vector2(vX, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, vY);
            }


            if (typeMov == TypeMov.Horizontal)
            {
                if (isHittingWall() || isHittingEdge())
                {
                    if (isHittingBothWalls())
                    {
                        isStuck = true;
                    }
                    else
                    {

                        if (facingDirection == LEFT)
                        {
                            ChangeFacingDirection(RIGHT);
                        }
                        else if (facingDirection == RIGHT)
                        {
                            ChangeFacingDirection(LEFT);
                        }
                    }
                }
            }
            else
            {
                if (isHittingWall())
                {
                    // print("wall hit");
                    if (facingDirection == UP)
                    {
                        ChangeFacingDirection(DOWN);
                    }
                    else if (facingDirection == DOWN)
                    {
                        ChangeFacingDirection(UP);
                    }
                }
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
        }
        
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if(newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }
        else if (newDirection == UP)
        {
            newScale.y = baseScale.y;
        }
        else if (newDirection == DOWN)
        {
            newScale.y = -baseScale.y;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    bool isHittingWall()
    {
        bool val = false;
        float castDist = baseCastDist;
        if(facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;

        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Vector3 backtargetPos = castPos.position;
        backtargetPos.x -= castDist;


        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        Debug.DrawLine(castPos.position, backtargetPos, Color.yellow);


        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;


    }

    bool isHittingBothWalls()
    {
        bool val = false;
        float castDist = baseCastDist;
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;

        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;
        Vector3 backtargetPos = castPos.position;
        backtargetPos.x -= castDist*12;


        Debug.DrawLine(castPos.position, targetPos, Color.blue);
        Debug.DrawLine(castPos.position, backtargetPos, Color.yellow);


        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")) && Physics2D.Linecast(castPos.position, backtargetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;


    }


    bool isHittingEdge()
    {
        bool val = true;
        float castDist = baseCastDist;
        

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);


        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerDeath(collision);
        }
    }

    private void PlayerDeath(Collider2D col)
    {
        Animator animator = col.gameObject.GetComponent<Animator>();
        //col.gameObject.GetComponent<platformer>().enabled = false;
        animator.SetBool("isJumping", false);
        if (!animator.GetBool("isDead"))
        {
            soundManager.Jump();
            animator.SetBool("isDead", true);
            
        }
        
    }


}
