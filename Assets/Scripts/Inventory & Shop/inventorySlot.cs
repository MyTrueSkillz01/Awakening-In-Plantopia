using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class inventorySlot : MonoBehaviour, IPointerClickHandler
{
    public itemSO itemSO;
    public int quantity;

    public Image itemImage;
    public TMP_Text quantityText;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(itemSO != null && quantity > 0)
        {
            // Changed from Left to Right
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.UseItem(this);
            }
        }
    }

    public void UpdateUI()
    {
        if (itemSO != null && quantity > 0)
        {
            itemImage.sprite = itemSO.itemIcon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = quantity.ToString();
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}