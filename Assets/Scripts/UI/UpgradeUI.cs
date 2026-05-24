using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image iconImage;

    private CardData currentCard;
    private Action<CardData> onCardClicked; 

   
    public void Setup(CardData card, Action<CardData> onClickCallback)
    {
        currentCard = card;
        nameText.text = card.upgradeName;
        iconImage.sprite = card.icon;

        onCardClicked = onClickCallback;
    }

    
    public void OnClick()
    {
        if (currentCard != null)
        {
            onCardClicked?.Invoke(currentCard); 
        }
    }
}