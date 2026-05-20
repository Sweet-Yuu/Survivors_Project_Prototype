using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] Image cardImageRenderer;
    [SerializeField] TextMeshProUGUI cardTextRenderer;

    private CardSO cardInfo;
    public void Setup(CardSO card)
    {
        cardInfo = card;    
        cardImageRenderer.sprite = card.cardImage;
        cardTextRenderer.text = card.cardText;
    }
}
