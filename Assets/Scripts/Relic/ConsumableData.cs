// ConsumableData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumable", menuName = "Game Data/Consumable Data")]
public class ConsumableData : ScriptableObject, IShopItem
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [TextArea][SerializeField] private string itemDescription;
    [SerializeField] private int basePrice;

    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public string ItemDescription => itemDescription;
    public int BasePrice => basePrice;

    public void Consume()
    {
        Debug.Log($"Đã dùng vật phẩm tiêu hao dự phòng: {itemName}");
    }
}