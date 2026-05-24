using UnityEngine;

public enum CardRarity
{
    Common,
    Rare,
    Legendary
}

public abstract class CardData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public CardRarity rarity; 
    public int weight = 50;   

    public abstract void ApplyUpgrade(PlayerStats player);
}