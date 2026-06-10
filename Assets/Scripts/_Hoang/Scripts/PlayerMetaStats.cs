using UnityEngine;
using System.Reflection;

public class PlayerMetaStats : PlayerStats
{
    [Header("Meta Progression Nodes (Kéo thả file từ Thẻ 2 vào đây)")]
    public MetaUpgradeNode hpUpgradeNode;
    public MetaUpgradeNode dmgUpgradeNode;
    public MetaUpgradeNode defUpgradeNode;
    // Có thể khai báo thêm nếu có các Node khác...

    private void Start()
    {
        // 1. Khởi tạo chỉ số gốc từ PlayerData (Kế thừa logic cũ)
        hp = Player.Instance.PlayerData.maxHealth;
        dmg = Player.Instance.PlayerData.damage;
        spd = Player.Instance.PlayerData.attackSpd;
        crit = Player.Instance.PlayerData.critChance;
        critDmg = Player.Instance.PlayerData.critDmg;
        def = Player.Instance.PlayerData.maxDef;
        move = Player.Instance.PlayerData.moveSpeed;

        // ------------------------------------------------------------------
        // CHÚ Ý: Logic cộng dồn chỉ số nâng cấp tạm thời bị vô hiệu hóa.
        // Sau khi hoàn thành "Thẻ số 3" (Xây dựng Manager để Save/Load cấp độ),
        // chúng ta sẽ quay lại đây viết code lấy cấp độ hiện tại và cộng dồn
        // mảng statBonusPerLevel[] vào các chỉ số ở trên.
        // ------------------------------------------------------------------

        // Đồng bộ lượng máu gốc sang PlayerHealth (Sử dụng Reflection để lách qua 'private set')
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