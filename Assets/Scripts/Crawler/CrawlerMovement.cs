using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(SpriteRenderer))]

public class CrawlerMovement : MonoBehaviour
{

    AudioManager audioManager;
    GameObject bit;
    public GameObject bitpos;
    public GameObject eye;
    public ParticleSystem dust;
    public bool isPowered = true;
    public bool canMove = true;
    private bool facingRight;

    [Header("Horizontal Speed")]
    public float speedX;
    public float groundAccelerationX;
    float targetSpeedX;
    float currentSpeedX;
    private float moveX;

    [Header ("Jump")]
    public float airAccelerationX;
    public float jumpHeight;
    public float timeToApex;
    public float lowJumpMultiplier;
    public float highJumpMultiplier;
    public LayerMask[] collisionMasks;
    LayerMask collisionMask;
    bool spacePressed = false;
    bool spaceHolded = false;
    float jumpSpeed;
    float coyoteTimer = 0.15f;
    bool hasCoyoteJump = false;
    float jumpBufferTimer = 0f;

    public ParticleSystem launch_fx;

    //the onAnotherCrawler is used to stop crawlers from jumping on top of ungrounded crawlers below them
    public bool onAnotherCrawler;
    public bool crawlerBelowIsGrounded;
    public bool grounded;
    public bool Grounded {
        get {
            return grounded;
        }
        set {
            if (value == grounded)
                return;

            grounded = value;
            if (grounded) {
                CreateDust();
                CheckBufferTime();
                audioManager.Play("crawlerLand", 0);
            }
        }
    }


    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    SpriteRenderer eyesr;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        bit = GameObject.FindGameObjectWithTag("Bit");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        eyesr = eye.GetComponent<SpriteRenderer>();

        Physics2D.gravity = new Vector2 (0, -(2 * jumpHeight)/Mathf.Pow(timeToApex, 1.5f));
        jumpSpeed = Mathf.Abs(Physics2D.gravity.y) * timeToApex;

        foreach(LayerMask mask in collisionMasks) {
            collisionMask.value |= mask.value;
        }

        facingRight = !sr.flipX;
        grounded = true;
    }

    void Update()
    {

        if (isPowered) {

            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            targetSpeedX = Input.GetAxis("Horizontal") * speedX;

            //Applies a small amount of acceleration and deceleration depending if grounded or not
            if(isGrounded())
                moveX = Mathf.SmoothDamp(rb.velocity.x, targetSpeedX, ref currentSpeedX, groundAccelerationX);
            else
                moveX = Mathf.SmoothDamp(rb.velocity.x, targetSpeedX, ref currentSpeedX, airAccelerationX);

            if(Input.GetKeyDown(KeyCode.Space) && !GameManager.inputBlocked) {
                spacePressed = true;
            }
 
            if (Input.GetKey(KeyCode.Space) && !GameManager.inputBlocked) {
                spaceHolded = true;
            }

            if(Input.GetKeyUp(KeyCode.Space) && !GameManager.inputBlocked) {
                spaceHolded = false;
            }

            anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
            anim.SetFloat("speedY", rb.velocity.y);
        } else
        {
            ManageInertia();
        }

    }

    void FixedUpdate() {
        Grounded = isGrounded();

        //Manages jump height based on space key
        if (!Grounded) {
            if (rb.velocity.y > 0 && !spaceHolded) {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            } else if (rb.velocity.y > 0 && spaceHolded) {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (highJumpMultiplier - 1) * Time.deltaTime;
            } else {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (highJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        CheckCoyote();

        if (canMove) {
            if (spacePressed && !isGrounded()) {
                jumpBufferTimer = 0.1f;
            }

            if (!isGrounded() && jumpBufferTimer > 0f) {
                jumpBufferTimer -= Time.deltaTime;
            }

            if (spacePressed && crawlerBelowIsGrounded && (isGrounded() || (hasCoyoteJump && rb.velocity.y < 0))) {
                Jump();
                audioManager.Play("crawlerJump", 0);
            }

            if (Mathf.Abs(rb.velocity.x) > 1f && Grounded) {
                audioManager.Play("crawlerWalk", 0);
            } else {
                audioManager.Stop("crawlerWalk");
            }

            Vector2 movement = new Vector2(moveX, rb.velocity.y);
            rb.velocity = movement;

            Flip(targetSpeedX);
        } else {
            ManageInertia();
        }


        spacePressed = false;
    }

    private void Jump() {
        CreateDust();
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        hasCoyoteJump = false;
        spacePressed = false;
    }

    private void ManageInertia()
    {

        if(Grounded)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        } else {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

    //Handles coyote time
    private void CheckCoyote() {
        if (isGrounded()) {
            coyoteTimer = 0.15f;
            hasCoyoteJump = true;
        } else {
            if (coyoteTimer > 0)
                coyoteTimer -= Time.deltaTime;
            else
                hasCoyoteJump = false;
        }
    }

    private void CheckBufferTime() {
        if (jumpBufferTimer > 0 && crawlerBelowIsGrounded) {
            Jump();
        }
    }

    void CreateDust() {
        dust.Play();
    }

    private void Flip(float horizontal)
    {

        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            sr.flipX = !facingRight;
            eyesr.flipX = sr.flipX;

        }

    }

    private bool isGrounded()
    {
        bool grounded = true;

        Vector2 origin1 = new Vector2(GetComponent<BoxCollider2D>().bounds.min.x + 0.05f, GetComponent<BoxCollider2D>().bounds.min.y - 0.05f);
        Vector2 origin2 = new Vector2(GetComponent<BoxCollider2D>().bounds.max.x - 0.05f, GetComponent<BoxCollider2D>().bounds.min.y - 0.05f);

        //In case of issues with grounding, distance should be 0.1
        RaycastHit2D hit1 = Physics2D.Raycast(origin1, Vector2.down, 0.01f, collisionMask);
        RaycastHit2D hit2 = Physics2D.Raycast(origin2, Vector2.down, 0.01f, collisionMask);
        
        if (hit1 || hit2)
            grounded = true;
        else
            grounded = false;

        crawlerBelowIsGrounded = true;

        if (hit1 || hit2) {
            if ((hit1 && hit1.collider.gameObject.tag == "Player") || (hit2 && hit2.collider.gameObject.tag == "Player")) {
                onAnotherCrawler = true;
                if (hit1 && hit1.collider.gameObject.tag == "Player")
                    BelowIsGrounded(hit1.collider.gameObject);
                else if (hit2 && hit2.collider.gameObject.tag == "Player")
                    BelowIsGrounded(hit2.collider.gameObject);
            } else
                onAnotherCrawler = false;

        }

        anim.SetBool("isGrounded", grounded);
        return grounded;
    }

    private void BelowIsGrounded(GameObject crawler) {
        if (onAnotherCrawler) {
            if (crawler.GetComponent<CrawlerMovement>().Grounded)
                crawlerBelowIsGrounded = true;
            else
                crawlerBelowIsGrounded = false;
        }
    }

    //Invoked by bit, makes sure there's some delay before player is powered and can move
    public void PowerToggle(){
        isPowered = true;
    }

    public void MoveToggle() {
        canMove = true;
    }

}
