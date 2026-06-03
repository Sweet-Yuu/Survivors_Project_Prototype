using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [System.Serializable]
    public struct RarityWeight
    {
        public CardRarity rarity;
        public int weight;
    }

    [Header("Data")]
    public PlayerStats playerStats;
    public List<CardData> allCards;

    [Header("Rarity Settings")]
    public List<RarityWeight> rarityWeights;

    [Header("UI")]
    public GameObject upgradePanel;
    public UpgradeUI[] uiSlots;

    [Header("DOTween Settings")]
    [SerializeField] private float startYPosition = -1000f;
    [SerializeField] private float targetYPosition = 0f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float delayBetweenCards = 0.15f;

    // Pre-allocated list to prevent GC garbage inside the card selection loop
    private readonly List<CardData> _validCardsPool = new List<CardData>();

    public void ShowUpgradeSelection()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0f;

        // Group cards by rarity once per open
        var cardsByRarity = BuildCardsByRarityLookup();

        // STEP 1: Roll a SINGLE common rarity for this entire upgrade session
        CardRarity chosenRarity = RollCommonRarity(cardsByRarity);

        // STEP 2: Filter all available cards that match the chosen rarity
        _validCardsPool.Clear();
        if (cardsByRarity.TryGetValue(chosenRarity, out var completePool))
        {
            _validCardsPool.AddRange(completePool);
        }

        // STEP 3: Draw up to 3 unique cards from this specific rarity pool
        List<CardData> selection = new List<CardData>(3);
        int cardsToDraw = Mathf.Min(3, _validCardsPool.Count);

        while (selection.Count < cardsToDraw)
        {
            CardData card = GetRandomCardFromPool(selection);
            if (card != null)
            {
                selection.Add(card);
            }
        }

        // --- DOTween UI Animation Loop ---
        for (int i = 0; i < uiSlots.Length; i++)
        {
            if (i < selection.Count)
            {
                uiSlots[i].gameObject.SetActive(true);
                uiSlots[i].Setup(selection[i], HandleCardSelected);

                RectTransform rectTransform = uiSlots[i].GetComponent<RectTransform>();

                Vector2 startPos = rectTransform.anchoredPosition;
                startPos.y = startYPosition;
                rectTransform.anchoredPosition = startPos;

                CanvasGroup canvasGroup = uiSlots[i].GetComponent<CanvasGroup>();
                if (canvasGroup != null) canvasGroup.alpha = 0f;

                float cardDelay = i * delayBetweenCards;

                rectTransform.DOAnchorPosY(targetYPosition, duration)
                    .SetEase(Ease.OutBack)
                    .SetDelay(cardDelay)
                    .SetUpdate(true);

                if (canvasGroup != null)
                {
                    canvasGroup.DOFade(1f, duration)
                        .SetDelay(cardDelay)
                        .SetUpdate(true);
                }
            }
            else
            {
                uiSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void HandleCardSelected(CardData selectedCard)
    {
        selectedCard.ApplyUpgrade(playerStats);
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Rolls a single overall Rarity based on weights, checking if any cards are available
    private CardRarity RollCommonRarity(Dictionary<CardRarity, List<CardData>> cardsByRarity)
    {
        List<int> availableIndices = new List<int>();
        int totalRarityWeight = 0;

        for (int i = 0; i < rarityWeights.Count; i++)
        {
            CardRarity rarity = rarityWeights[i].rarity;
            // Only consider rarities that actually contain cards in your data pool
            if (cardsByRarity.TryGetValue(rarity, out var cards) && cards.Count > 0)
            {
                availableIndices.Add(i);
                totalRarityWeight += rarityWeights[i].weight;
            }
        }

        // Fallback safety if weights are missing or unassigned
        if (totalRarityWeight == 0) return CardRarity.Common;

        int roll = Random.Range(0, totalRarityWeight);
        int accumulated = 0;

        foreach (int idx in availableIndices)
        {
            accumulated += rarityWeights[idx].weight;
            if (roll < accumulated)
            {
                return rarityWeights[idx].rarity;
            }
        }

        return rarityWeights[availableIndices[0]].rarity;
    }

    // Weighted random selection of a single card from the pre-filtered rarity pool
    private CardData GetRandomCardFromPool(List<CardData> currentSelection)
    {
        int totalCardWeight = 0;

        // Calculate total weight of non-selected cards inside the pool
        foreach (var card in _validCardsPool)
        {
            if (!currentSelection.Contains(card))
            {
                totalCardWeight += card.weight;
            }
        }

        if (totalCardWeight == 0) return null;

        int cardRoll = Random.Range(0, totalCardWeight);
        int current = 0;

        foreach (var card in _validCardsPool)
        {
            if (!currentSelection.Contains(card))
            {
                current += card.weight;
                if (cardRoll < current) return card;
            }
        }

        return null;
    }

    private Dictionary<CardRarity, List<CardData>> BuildCardsByRarityLookup()
    {
        var dict = new Dictionary<CardRarity, List<CardData>>();
        foreach (var card in allCards)
        {
            if (!dict.TryGetValue(card.rarity, out var list))
            {
                list = new List<CardData>();
                dict[card.rarity] = list;
            }
            list.Add(card);
        }
        return dict;
    }
}