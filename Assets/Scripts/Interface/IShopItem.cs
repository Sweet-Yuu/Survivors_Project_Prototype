// IShopItem.cs
using UnityEngine;

public interface IShopItem
{
    string ItemName { get; }

    Sprite ItemIcon { get; }

    string ItemDescription { get; }

    int BasePrice { get; }

}