using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]
public class itemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    [TextArea]public string itemDescription;

    public bool isGold;

    [Header("Item Stats")]
    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;

    [Header("For Temporary items")]
    public float duration;
}
