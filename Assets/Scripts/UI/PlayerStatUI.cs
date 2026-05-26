using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    [Header("References")]
    public PlayerStats playerStats; 
    public PlayerData baseData;    

    [Header("UI Text Elements")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI critText;
    public TextMeshProUGUI critDmgText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI moveText;

   
    public void RefreshUI()
    {
        if (playerStats == null || baseData == null) return;

        if (hpText != null) hpText.text = $"HP: {playerStats.hp} / {baseData.maxHealth}";
        if (dmgText != null) dmgText.text = $"DMG: {playerStats.dmg} / {baseData.damage}";
        if (spdText != null) spdText.text = $"ATK SPD: {playerStats.spd} / {baseData.attackSpd}";
        if (critText != null) critText.text = $"CRIT: {playerStats.crit}% / {baseData.critChance}%";
        if (critDmgText != null) critDmgText.text = $"CRIT DMG: {playerStats.critDmg}% / {baseData.critDmg}%";
        if (defText != null) defText.text = $"DEF: {playerStats.def} / {baseData.maxDef}";
        if (moveText != null) moveText.text = $"MOVE: {playerStats.move} / {baseData.moveSpeed}";
    }
}