using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public float deathAnimationDuration = 1f; // Adjust this to match your animation length
    
    private Animator anim;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void ChangeHealth(int amount)
    {
        // Don't take damage if already dead
        if(isDead) return;
        
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isDead = true;
        
        // Play death animation
        if(anim != null)
        {
            anim.Play("dead");
        }
        
        // Disable enemy AI and movement
        if(GetComponent<enemyMovement>() != null)
        {
            GetComponent<enemyMovement>().enabled = false;
        }
        
        if(GetComponent<enemyDamage>() != null)
        {
            GetComponent<enemyDamage>().enabled = false;
        }
        
        // Disable collider so player can't hit dead enemy
        if(GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        
        // Stop movement
        if(GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        
        // Destroy after animation finishes
        StartCoroutine(DestroyAfterAnimation());
    }
    
    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for death animation to finish
        yield return new WaitForSeconds(deathAnimationDuration);
        
        Destroy(gameObject);
    }
}