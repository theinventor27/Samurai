using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    public float moveSpeed = 5f; // control movement speed.
    public float jumpForce = 10f; // Control Jump force.
    public bool isGrounded;
    public LayerMask groundLayer;

    private Animator animator;
    public bool meleeInput;
    private bool attacking;
    public bool onWall;
    private float wallSlidingSpeed = .5f;
    private float attackDelay = .4f;
    public bool isMagicAttacking;
    public bool jumpInput;
    public GameObject fireballPrefab;
    public Transform playerHand;
    private Rigidbody2D rb;
    private float moveSpeedDefault;
    int attackState = 0;


    //Animation States
    private string currentState;
    const string Hero_Idle = "Hero_Idle";
    const string Hero_Run = "Hero_Run";
    const string Hero_Jump = "Hero_Jump";
    const string Hero_OnWall = "Hero_OnWall";
    const string Hero_WallJump = "Hero_WallJump";

    const string Hero_MeleeAttack0 = "Hero_MeleeAttack0";
    const string Hero_MeleeAttack1 = "Hero_MeleeAttack1";
    const string Hero_MeleeAttack2 = "Hero_MeleeAttack2";

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveSpeedDefault = moveSpeed;
    }

    void Update()
    {
        FlipSprite(horizontalInput);

        //JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            meleeInput = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isMagicAttacking = true;
            animator.SetBool("isMagicAttacking", true);
        }
        else
        {
            isMagicAttacking = false;
            animator.SetBool("isMagicAttacking", false);
        }
        WallSlide();
    }
    private void FixedUpdate()
    {
        CheckGrounded();
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(moveSpeed * Time.deltaTime * new Vector2(horizontalInput, 0f));

        // Check if the player is moving
        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f;

        if (isGrounded && !attacking)
        {
            // Flip the sprite based on the movement direction
            if (isMoving)
            {
                ChangeAnimationState(Hero_Run);

            }
            else
            {
                ChangeAnimationState(Hero_Idle);
            }
        }

        if (jumpInput && (isGrounded || onWall))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpInput = false;
            if (!onWall)
            {
                ChangeAnimationState(Hero_Jump);
            }
        }


        if (meleeInput)
        {
            meleeInput = false;
            if (!attacking)
            {
                attacking = true;
                if (isGrounded)
                {
                    attackState = (attackState + 1) % 3;

                    ChangeAnimationState("Hero_MeleeAttack" + attackState);
                }
                Invoke("AttackComplete", attackDelay);
            }
        }





    }
    void AttackComplete()
    {
        attacking = false;
    }


    //Check if player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    //Wall Sliding and Wall Jumping
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 1f, wallLayer);
    }
    private void WallSlide()
    {
        if (!isGrounded && IsWalled() && horizontalInput != 0)
        {
            ChangeAnimationState(Hero_OnWall);
            onWall = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            onWall = false;
        }
    }
    public void StopPlayerMovement()
    {
        moveSpeed = 0;
    }
    public void resetPlayerMovement()
    {
        moveSpeed = moveSpeedDefault;
    }
    public void SpawnFireball()
    {
        GameObject newFireball = Instantiate(fireballPrefab, playerHand.position, playerHand.rotation);
        Destroy(newFireball, 3f); // Destroy the fireball after 3 seconds
    }



    // Flip the sprite based on the movement direction
    void FlipSprite(float horizontalInput)
    {
        // Flip the sprite if moving left
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
        }
        // Unflip the sprite if moving right
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // No horizontal input, maintain the current scale
    }
    void HandleAttack()
    {
        // Increment attack state in a cyclic manner (0 -> 1 -> 2 -> 0 -> 1 -> 2 -> ...)
        attackState = (attackState + 1) % 3;

        // Trigger the corresponding animation
        animator.SetInteger("MeleeAttackState", attackState);
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;
        //Play animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    private void CheckGrounded()
    {
        // Set the length of the raycast
        float raycastLength = 1f;

        // Cast a ray straight down to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayer);

        // If the ray hits something, the player is grounded
        isGrounded = hit.collider != null;

        // Debug logs for testing
        if (isGrounded)
        {
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("Not Grounded");
        }
    }



}
