using TMPro;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        healthText.text = "HP : " + currentHealth + " / " + maxHealth;
    }

    public void changeHealth(int amount)
    {
        currentHealth += amount;
        
        // Clamp health between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthTextAnim.Play("TextUpdate");
        healthText.text = "HP : " + currentHealth + " / " + maxHealth;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}