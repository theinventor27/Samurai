using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //States
    public float chaseRange = 10f;
    public float attackRange = 3f;

    public Transform playerTarget;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;
    public float moveSpeedDefault;
    private Transform target;
    private Animator animator;

    public int Health;
    internal int health;

    public string State = "";

    void Awake()
    {
        animator = GetComponent<Animator>();
        target = pointB;
        State = "Patrol";
        moveSpeedDefault = moveSpeed;

    }

    void Update()
    {
        //Patrol
        if (State == "Patrol")
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                target = (target == pointA) ? pointB : pointA;

                // Check the target and set the facing direction accordingly
                if (target == pointA)
                {
                    // Set facing direction for pointA
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    // Set facing direction for pointB
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
        }
        //Chasing
        if (Vector3.Distance(transform.position, playerTarget.position) <= chaseRange )
        {
            State = "Chasing";

            // Only change the x-axis position
            float moveDirection = Mathf.Sign(playerTarget.position.x - transform.position.x);
            transform.position = new Vector2(Vector3.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime).x, transform.position.y);

            // Flip the sprite based on the movement direction
            transform.localScale = new Vector3(moveDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        //Attacking
        if (Vector3.Distance(transform.position, playerTarget.position) <= attackRange)
        {
            State = "Attacking";
            animator.SetBool("isAttacking", true);
        }
        //Player gets out of attacking range    
        if (Vector3.Distance(transform.position, playerTarget.position) > attackRange &&  State == "Chasing")
        {
            animator.SetBool("isAttacking", false);

        }

    }


    public void IsAttacking()
    {
        moveSpeed = 0f;
        State = "Attacking";
    }
    public void DoneAttacking()
    {
        moveSpeed = moveSpeedDefault;
        State = "Chasing";

    }
}
