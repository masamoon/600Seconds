using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserScript : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";

    const string UP = "up";
    const string DOWN = "down";

    bool canAlert;

    public SoundManager soundManager;

    public enum State { Patrol,Chase };
    State currentState;

    Vector3 baseScale;

    [SerializeField]
    Transform castPos;

    [SerializeField]
    Transform castPos_ver;

    [SerializeField]
    float baseCastDist;

    [SerializeField]
    float agroDist;

    [SerializeField]
    CircleCollider2D visionRadius;

    string facingDirection_hor;
    string facingDirection_ver;


    float playerDist;

    Rigidbody2D rb2d;
    float moveSpeed = 2;

    public Animator animator;

    private void Start()
    {

        canAlert = true;
        baseScale = transform.localScale;

        facingDirection_hor = RIGHT;
        facingDirection_ver = UP;

        rb2d = GetComponent<Rigidbody2D>();

        currentState = State.Patrol;

        soundManager = GetComponent<SoundManager>();

        //animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        
        if(currentState == State.Chase)
        {
            animator.SetBool("isChasing", true);
            ChasePlayer();
            canAlert = false;
        }
        else
        {
            animator.SetBool("isChasing", false);
        }

        float vX = moveSpeed;

        if (facingDirection_hor == LEFT)
        {
            vX = -moveSpeed;
        }
        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

        if (isHittingWall_hor())
        {
            //print("wall hit");
            if (facingDirection_hor == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else if (facingDirection_hor == RIGHT)
            {
                ChangeFacingDirection(LEFT);
            }

            
        }

        if (isHittingWall_ver())
        {
            if (facingDirection_hor == UP)
            {
                ChangeFacingDirection(DOWN);
            }
            else if (facingDirection_hor == DOWN)
            {
                ChangeFacingDirection(UP);
            }
        }

        
    }

    private void Update()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), agroDist);

        bool playerInCircle = false;

        foreach (Collider2D c in col) {
            if (c.gameObject.tag == "Player")
            {
                playerInCircle = true;
                currentState = State.Chase;
                //print("dist from player: " + Vector2.Distance(c.gameObject.transform.position, gameObject.transform.position));
            }
        }
        if(!playerInCircle)
        {
            currentState = State.Patrol;
            animator.SetBool("isChasing", false);
            canAlert = true;
        }
    }

    void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection_hor = newDirection;
    }

    bool isHittingWall_hor()
    {
        bool val = false;
        float castDist = baseCastDist;
        if (facingDirection_hor == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;

        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        //Debug.DrawLine(castPos.position, targetPos, Color.blue);


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

    bool isHittingWall_ver()
    {
        bool val = false;
        float castDist = baseCastDist;
        if (facingDirection_ver == DOWN)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;

        }

        Vector3 targetPos = castPos_ver.position;
        targetPos.y += castDist;

        //Debug.DrawLine(castPos.position, targetPos, Color.blue);


        if (Physics2D.Linecast(castPos_ver.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
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

        //Debug.DrawLine(castPos.position, targetPos, Color.red);


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

    void ChasePlayer()
    {
        if (canAlert)
        {
            soundManager.Jump();
        }
        print("chasing player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, 2 * Time.deltaTime);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerDeath(collision);
        }
    }

    
    

    private void PlayerDeath(Collider2D col)
    {
        Animator animatorPlayer = col.gameObject.GetComponent<Animator>();
        //col.gameObject.GetComponent<platformer>().enabled = false;
        animatorPlayer.SetBool("isJumping", false);
        if (!animatorPlayer.GetBool("isDead"))
        {
           // print("killed by guardian");
            soundManager.Footsteps();
            animatorPlayer.SetBool("isDead", true);

        }

        //Destroy(col.gameObject);
    }

}
