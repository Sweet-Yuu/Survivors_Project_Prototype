using UnityEngine;

[CreateAssetMenu(fileName = "NewMetaUpgrade", menuName = "Moonblight/Meta Upgrade Node")]
public class MetaUpgradeNode : ScriptableObject
{
    [Header("Thông tin cơ bản")]
    public string upgradeName;
    public int maxLevel;

    [Header("Chi phí và Chỉ số theo Cấp (Size = Max Level)")]
    public int[] costPerLevel;
    public float[] statBonusPerLevel;
}