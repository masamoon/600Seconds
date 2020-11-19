using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class platformer : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;

    public float jumpforce;

    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f;

    public GameObject score;

    int fuel;

    int jumpcost;

    public FuelBar fuelbar;

    public GameObject gem;

    public GameObject bomb;

    public GameObject weightText;

    public GameObject bombText;

    public GameObject portal;

    
    public float invincibilityDurationSeconds;

    private bool facingRight;

    public Animator animator;

    public SoundManager soundManager;

    public bool isHovering;

    private bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;

        isHovering = false;

        soundManager = GetComponent<SoundManager>();
        rb = GetComponent<Rigidbody2D>();

        facingRight = true;
        //fuel = 100;

        fuelbar = GameObject.FindGameObjectWithTag("FuelBar").GetComponent<FuelBar>();

        if (PlayerStats.getMaxFuel() > 0)
        {
            fuelbar.setFuel(PlayerStats.getMaxFuel());
            fuel = PlayerStats.getMaxFuel();
            
        }
        else
        {
            fuelbar.setFuel(100);
            fuel = 100;
        }

        

        jumpcost = 1;

        weightText = GameObject.FindGameObjectWithTag("Weight");
        bombText = GameObject.FindGameObjectWithTag("Bombs");
        //invincibilityDurationSeconds = 2.5f;


    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isDead"))
        {
            isDead = true;
           // print("dead");
            StartCoroutine(WaitAfterDeath(3f));

        }

        if (!isDead)
        {
            Move();
            Jump();
            BetterJump();
            ThrowGem();
            ThrowBomb();
            Hover();
        }
        fuelbar.setFuel(fuel);

        jumpcost = Score.score / 1000 + 1;
       
        Text wT = weightText.GetComponent<Text>();
        wT.text = jumpcost.ToString();

        Text bT = bombText.GetComponent<Text>();
        bT.text = PlayerStats.getBombs().ToString();

        if (PlayerStats.getChestOpened())
        {
            GameObject exitPortal = Instantiate(portal, gameObject.transform.position, Quaternion.identity);
            PlayerStats.setChestOpened(false);
        }

               

    }

    
    void Move() { 
        float x = Input.GetAxisRaw("Horizontal");


        if (x < 0 && facingRight)
        {
            
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }
        else if ( x > 0 && !facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }

        float moveBy = x * speed;

        animator.SetFloat("speed", Mathf.Abs(moveBy));

        rb.velocity = new Vector2(moveBy, rb.velocity.y); 
    }

    void Hover()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isJumping", true);
            //rb.velocity += Vector2.up * Physics2D.gravity * (0.01f - 1) * Time.deltaTime;
            float vX = rb.velocity.x;
            rb.velocity = new Vector2(vX, (float)(-1f * 9.8f * (7.50f - 1) * Time.deltaTime));
            isHovering = true;

        }
        else
        {
            isHovering = false;
            if(rb.velocity.y == 0)
                animator.SetBool("isJumping", false);
        }
    }
    
    void Jump(){
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (fuel > 1)
            {
                fuel = fuel - jumpcost;
                soundManager.Jump();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                animator.SetBool("isJumping", true);
            }
            else
            {
                fuel = 0;
            }
        } 

        
    }


    void BetterJump() {
        if (rb.velocity.y < 0 && !Input.GetKey(KeyCode.LeftShift)) {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
            animator.SetBool("isJumping", false);
        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z)) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }   
    }

    void ThrowGem()
    {
        if (Input.GetKeyDown(KeyCode.X) && Score.score>=100)
        {
            Debug.Log("throw gem");
            GameObject throwgem = Instantiate(gem, gameObject.transform.position, Quaternion.identity);
            throwgem.GetComponent<Pickup>().catchable = false;
            Rigidbody2D throwgemrb2d = throwgem.GetComponent<Rigidbody2D>();
            Score.score -= 100;
            throwgemrb2d.bodyType = RigidbodyType2D.Dynamic;
            throwgemrb2d.AddForce(new Vector2(2,2) * 50);
            StartCoroutine(BecomeTemporarilyInvincible(throwgem));
            
        }
    }

    void ThrowBomb()
    {
        //if (Input.GetKeyDown(KeyCode.C) && Score.bombs>0)
        if (Input.GetKeyDown(KeyCode.C) && PlayerStats.getBombs() > 0)
        {
            float directioninput_h = Input.GetAxis("Horizontal");
            float directioninput_v = Input.GetAxis("Vertical");
            GameObject throwbomb = Instantiate(bomb, gameObject.transform.position, Quaternion.identity);
            Rigidbody2D throwbombrb2d = throwbomb.GetComponent<Rigidbody2D>();
            throwbombrb2d.bodyType = RigidbodyType2D.Dynamic;
            //throwbombrb2d.AddForce(new Vector2(2, 2) * 50);
            throwbombrb2d.AddForce(new Vector2(directioninput_h, directioninput_v) * 100);
            //Score.bombs -= 1;
            PlayerStats.setBombs(PlayerStats.getBombs() - 1);


        }
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
        Debug.Log("Gem not catchable");
        throwgem.GetComponent<Pickup>().catchable = false;

        yield return new WaitForSeconds(invincibilityDurationSeconds);

        Debug.Log("Gem catchable");
        throwgem.GetComponent<Pickup>().catchable = true;
    }

    private IEnumerator WaitAfterDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        isDead = true;
        SceneManager.LoadScene("GameOver");
    }


}
