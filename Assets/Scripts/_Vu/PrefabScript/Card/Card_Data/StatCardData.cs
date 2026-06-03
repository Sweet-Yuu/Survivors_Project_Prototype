using UnityEngine;

[CreateAssetMenu(fileName = "NewStatCard", menuName = "Data/Cards/Stat Card")]
public class StatCardData : CardData
{
    public StatType statType;
    public float upgradeValue;

    
    public override void ApplyUpgrade(PlayerStats player)
    {
        player.AddStats(statType, upgradeValue);
    }
}