// RelicData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewRelic", menuName = "Game Data/Relic Data")]
public class RelicData : ScriptableObject, IShopItem
{
    [Header("--- IShopItem Properties ---")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [TextArea(3, 5)]
    [SerializeField] private string itemDescription;
    [SerializeField] private int basePrice;

    // Định nghĩa rõ các biến tương ứng kế thừa từ IShopItem [cite: 11]
    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public string ItemDescription => itemDescription;
    public int BasePrice => basePrice;

    [Header("--- Relic Specific Properties ---")]
    
    [Tooltip("Mã định danh duy nhất (VD: RL_BloodChalice)[cite: 12]. Cực kỳ quan trọng để truy xuất Fixed Relic[cite: 13].")]
    public string relicID;

    // Phân loại Relic [cite: 12]
    public RelicCategory category;

    // Phần móc nối logic: virtual method để buff/debuff chỉ số vào PlayerStatsManager [cite: 14]
    public virtual void ApplyRelicEffect()
    {
        Debug.Log($"Đã kích hoạt hiệu ứng của Relic: {itemName}");
        // Hệ thống Event/Action để tác động vào PlayerStatsManager sẽ được viết đè (override) ở đây [cite: 14]
    }
}