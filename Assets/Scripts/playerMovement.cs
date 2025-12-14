using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;

    private bool isKnockedBack;
    public playerCombat player_Combat;

    // Store last direction for attacks
    private float lastHorizontal = 0;
    private float lastVertical = -1; // Default facing down

    private void Update()
    {
        // ADDED: Don't allow attacks when game is paused
        if(Time.timeScale == 0) return;
        
        // Left mouse button to attack
        if (Input.GetMouseButtonDown(0))
        {
            player_Combat.Attack();
        }
    }

    void FixedUpdate()
    {
        // ADDED: Don't move when game is paused
        if(Time.timeScale == 0) return;
        
        if(isKnockedBack == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Update last direction when moving
            if(horizontal != 0 || vertical != 0)
            {
                lastHorizontal = horizontal;
                lastVertical = vertical;
            }

            // Set current movement for walk animations
            anim.SetFloat("horizontal", horizontal);
            anim.SetFloat("vertical", vertical);

            // ALWAYS set last direction for blend tree
            anim.SetFloat("lastHorizontal", lastHorizontal);
            anim.SetFloat("lastVertical", lastVertical);

            rb.linearVelocity = new Vector2(horizontal, vertical) * speed; 
        }
    }

    public void Knockback(Transform enemy, float force, float stuntTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockBackCounter(stuntTime));
    }

    IEnumerator KnockBackCounter(float stuntTime)
    {
        yield return new WaitForSeconds(stuntTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }
}