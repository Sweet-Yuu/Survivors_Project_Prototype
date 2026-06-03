using Unity.Mathematics;
using UnityEngine;
public enum StatType { MaxHP, Dmg, AttackSpd, CritChance, CritDmg, Def, MoveSpd }
[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public float hp;
    public float dmg;
    public float spd;
    public float crit;
    public float critDmg;
    public float def;
    public float move;

    private void Start()
    {
        hp = Player.Instance.PlayerData.maxHealth;
        dmg = Player.Instance.PlayerData.damage;
        spd = Player.Instance.PlayerData.attackSpd;
        crit = Player.Instance.PlayerData.critChance;
        critDmg = Player.Instance.PlayerData.critDmg;
        def = Player.Instance.PlayerData.maxDef;
        move = Player.Instance.PlayerData.moveSpeed;
    }
    public void AddStats(StatType type, float amount)
    {
        switch (type)
        {
            case StatType.MaxHP: hp += amount; break;
            case StatType.Dmg: dmg += amount; break;
            case StatType.AttackSpd: spd += amount; break;
            case StatType.CritChance: crit += amount; break;
            case StatType.CritDmg: critDmg += amount; break;
            case StatType.Def: def += amount; break;
            case StatType.MoveSpd: move += amount; break;
        }
        Debug.Log($"Upgraded{type} + {amount}");
    }
}
