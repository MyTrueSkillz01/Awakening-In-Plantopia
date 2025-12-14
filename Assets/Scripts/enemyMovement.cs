using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = 1;
    private EnemyState enemyState;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        
        CheckForPlayer();
        
        // Move based on state
        if(enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if(enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if(enemyState == EnemyState.Idle)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Chase()
    {
        if(player == null) return;
        
        // Flip to face player
        if(player.position.x > transform.position.x && facingDirection == -1 ||
           player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        
        // Move toward player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        
        // Flip the sprite renderer instead of transform
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            sr.flipX = !sr.flipX;
        }
    }
    
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            // If in attack range AND cooldown is ready
            if(distanceToPlayer <= attackRange && attackCooldownTimer <= 0 && enemyState != EnemyState.Attacking)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            // If outside attack range, chase
            else if(distanceToPlayer > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            player = null;
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        // Don't change if already in this state
        if(enemyState == newState) return;
        
        // Exit old state animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);
            
        // Update state
        enemyState = newState;

        // Enter new state animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detectionPoint.position, attackRange);
    }
}

public enum EnemyState
{
    Idle, Chasing, Attacking
}