using UnityEngine;
using System.Reflection;

public class PlayerMetaStats : PlayerStats
{
    [Header("Meta Progression Integration")]
    [SerializeField] private MetaUpgradeSO metaUpgradeData;
    public MetaUpgradeSO MetaUpgradeData => metaUpgradeData;

    private void Start()
    {
        if (metaUpgradeData == null)
        {
            Debug.LogWarning("Chưa gán MetaUpgradeSO cho PlayerMetaStats!");
            return;
        }

        // 1. Gọi hàm Load mới của hệ thống Skill Tree
        metaUpgradeData.LoadSkillTree();

        // 2. Sử dụng hàm GetTotalBonus(StatType) mới để cộng dồn chỉ số
        hp += metaUpgradeData.GetTotalBonus(StatType.MaxHP);
        dmg += metaUpgradeData.GetTotalBonus(StatType.Dmg);
        def += metaUpgradeData.GetTotalBonus(StatType.Def);

        Debug.Log($"[META PROGRESSION] Đã kích hoạt: HP (+{metaUpgradeData.GetTotalBonus(StatType.MaxHP)}) | DMG (+{metaUpgradeData.GetTotalBonus(StatType.Dmg)})");

        // 3. Đồng bộ lượng máu đã nâng cấp sang PlayerHealth (Sử dụng Reflection để lách qua 'private set')
        PlayerHealth healthComponent = GetComponent<PlayerHealth>();
        if (healthComponent != null)
        {
            PropertyInfo currentHealthProp = typeof(PlayerHealth).GetProperty("CurrentHealth");
            if (currentHealthProp != null)
            {
                currentHealthProp.SetValue(healthComponent, hp);
            }
        }
    }
}