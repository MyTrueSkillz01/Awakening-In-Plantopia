using UnityEngine;

public class useItem : MonoBehaviour
{
    public playerHealth playerHealthScript;
    public playerMovement playerMovementScript;
    public playerCombat playerCombatScript;

    public void ConsumeItem(itemSO item, inventorySlot slot)
    {
        if(item == null) return;

        // Apply health effect
        if(item.currentHealth != 0)
        {
            playerHealthScript.changeHealth(item.currentHealth);
        }

        // Apply speed boost (temporary)
        if(item.speed != 0)
        {
            StartCoroutine(ApplySpeedBoost(item.speed, item.duration));
        }

        // Apply damage boost (temporary)
        if(item.damage != 0)
        {
            StartCoroutine(ApplyDamageBoost(item.damage, item.duration));
        }

        // Reduce quantity after use
        slot.quantity--;
        
        // Remove item from slot if quantity is 0
        if(slot.quantity <= 0)
        {
            slot.itemSO = null;
            slot.quantity = 0;
        }
        
        slot.UpdateUI();
    }

    private System.Collections.IEnumerator ApplySpeedBoost(int speedBoost, float duration)
    {
        float originalSpeed = playerMovementScript.speed;
        playerMovementScript.speed += speedBoost;
        
        Debug.Log($"Speed boosted by {speedBoost} for {duration} seconds!");
        
        yield return new WaitForSeconds(duration);
        
        playerMovementScript.speed = originalSpeed;
        Debug.Log("Speed boost ended!");
    }

    private System.Collections.IEnumerator ApplyDamageBoost(int damageBoost, float duration)
    {
        int originalDamage = playerCombatScript.damage;
        playerCombatScript.damage += damageBoost;
        
        Debug.Log($"Damage boosted by {damageBoost} for {duration} seconds!");
        
        yield return new WaitForSeconds(duration);
        
        playerCombatScript.damage = originalDamage;
        Debug.Log("Damage boost ended!");
    }
}