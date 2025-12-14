using UnityEngine;
using TMPro;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopUI;
    public TMP_Text promptText; // Optional: "Press F to open shop"
    
    private bool playerInRange = false;

    private void Start()
    {
        // Make sure shop is closed at start
        if(shopUI != null)
        {
            shopUI.SetActive(false);
        }
        
        // Hide prompt text at start
        if(promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // If player is in range and presses F
        if(playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ToggleShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
            
            // Show prompt text
            if(promptText != null)
            {
                promptText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;
            
            // Hide prompt text
            if(promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
            
            // Close shop if player walks away
            if(shopUI != null && shopUI.activeSelf)
            {
                CloseShop();
            }
        }
    }

    private void ToggleShop()
    {
        if(shopUI != null)
        {
            if(shopUI.activeSelf)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game when shop is open
        
        // Play doorbell sound
        if(AudioManager.instance != null && AudioManager.instance.doorBellSound != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.doorBellSound);
        }
        
        // Play shop music
        if(AudioManager.instance != null && AudioManager.instance.shopMusic != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.shopMusic);
        }
        
        // Hide prompt when shop is open
        if(promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        
        // Resume background music
        if(AudioManager.instance != null && AudioManager.instance.backgroundMusic != null)
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.backgroundMusic);
        }
        
        // Show prompt again if still in range
        if(playerInRange && promptText != null)
        {
            promptText.gameObject.SetActive(true);
        }
    }
}