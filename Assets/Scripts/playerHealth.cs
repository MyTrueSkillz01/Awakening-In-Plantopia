using TMPro;
using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;
    public Animator playerAnim; // ADD: Reference to player animator
    
    public GameManager gameManager;
    
    private bool isDead = false; // ADD: Track if player is dead

    private void Start()
    {
        healthText.text = "HP : " + currentHealth + " / " + maxHealth;
        
        // Get animator if not assigned
        if(playerAnim == null)
        {
            playerAnim = GetComponent<Animator>();
        }
    }

    public void changeHealth(int amount)
    {
        // Don't take damage if already dead
        if(isDead) return;
        
        currentHealth += amount;
        
        // Clamp health between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP : " + currentHealth + " / " + maxHealth;
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isDead = true;
        
        // Trigger death animation
        if(playerAnim != null)
        {
            playerAnim.SetBool("isDead", true);
        }
        
        // Disable player movement and combat
        GetComponent<playerMovement>().enabled = false;
        GetComponent<playerCombat>().enabled = false;
        
        // Show game over menu after animation
        StartCoroutine(ShowGameOverAfterAnimation());
    }
    
    private IEnumerator ShowGameOverAfterAnimation()
    {
        // Wait for death animation to finish (adjust time based on your animation length)
        yield return new WaitForSeconds(1.5f);
        
        if(gameManager != null)
        {
            gameManager.ShowGameOver();
        }
    }
}