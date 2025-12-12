using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public inventorySlot[] itemSlots;
    public int gold;
    public TMP_Text goldText;
    
    public useItem itemUser; // ADD THIS

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }

    private void OnEnable()
    {
        loot.onLootPickUp += AddItem;
    }

    private void OnDisable()
    {
        loot.onLootPickUp -= AddItem;
    }

    private void AddItem(itemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return; 
        }
        else
        {
            // Check if item already exists in inventory (for stacking)
            foreach (var slot in itemSlots)
            {
                if(slot.itemSO == itemSO)
                {
                    slot.quantity += quantity;
                    slot.UpdateUI();
                    return;
                }
            }

            // Add to empty slot if not found
            foreach (var slot in itemSlots)
            {
                if(slot.itemSO == null)
                {
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }
            }
        }
    }

    public void UseItem(inventorySlot slot)
    {
        if(slot.itemSO != null && slot.quantity > 0)
        {
            Debug.Log("Using item: " + slot.itemSO.itemName);
            itemUser.ConsumeItem(slot.itemSO, slot); // USE THE ITEM
        }
    }
}