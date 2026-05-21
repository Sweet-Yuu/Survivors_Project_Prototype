using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;

    [SerializeField] GameObject cardPrefab;

    [SerializeField] Transform cardContainer;

    [SerializeField] List<CardSO> deck;

    GameObject cardOne, cardTwo, cardThree;

    List<CardSO> alreadySelectedCards = new List<CardSO>();

    void Start()
    {
        randomizeCards();
    }

    void randomizeCards()
    {
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizeCards = new List<CardSO>();

        List<CardSO> availableCards = new List<CardSO>(deck);

        availableCards.RemoveAll(card => card.isUnique && alreadySelectedCards.Contains(card));

        if (availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
            return;

        }

        while (randomizeCards.Count < 3)
        {
            CardSO randomCards = availableCards[Random.Range(0, availableCards.Count)];

            if (!randomizeCards.Contains(randomCards))
            {
                randomizeCards.Add(randomCards);
            }
        }

        cardOne = InstantiateCard(randomizeCards[0], cardContainer); 
        cardTwo = InstantiateCard(randomizeCards[1] ,cardContainer); 
        cardThree = InstantiateCard(randomizeCards[2] , cardContainer); 

    }
        GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject cardGo= Instantiate(cardPrefab, position);
        Card card = cardGo.GetComponent<Card>();
        card.Setup(cardSO, this);
        return cardGo;
    }

    public void OnCardSelected(CardSO selectedCard)
    {
        if (!alreadySelectedCards.Contains(selectedCard))
        {
            alreadySelectedCards.Add(selectedCard);
        }
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        if(cardSelectionUI !=null)
        {
            cardSelectionUI.SetActive(false);
        }
        Debug.Log("Đã chọn thẻ: " + selectedCard.cardText + " | 3 thẻ bài đã biến mất.");
    }
}
