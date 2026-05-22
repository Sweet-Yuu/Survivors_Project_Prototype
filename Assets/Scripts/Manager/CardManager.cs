using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardContainer;
    [SerializeField] List<CardSO> deck;

    GameObject cardOne, cardTwo, cardThree;
    List<CardSO> alreadySelectedCards = new List<CardSO>();

    // Chú ý: Không dùng hàm Start() ở đây nữa vì thẻ bài chỉ xuất hiện khi Lên Cấp.
    // Việc gọi bốc bài sẽ do ResourceManager đảm nhận.

    public void StartLevelUpSelection()
    {
        // Xóa các thẻ bài cũ nếu có (để dọn chỗ cho lần lên cấp này)
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizeCards = new List<CardSO>();
        List<CardSO> availableCards = new List<CardSO>(deck);

        // Loại bỏ các thẻ Unique đã được bốc từ trước để không xuất hiện lại
        availableCards.RemoveAll(card => card.isUnique && alreadySelectedCards.Contains(card));

        if (availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
            return;
        }

        // Vòng lặp bốc ngẫu nhiên 3 thẻ không trùng nhau
        while (randomizeCards.Count < 3)
        {
            CardSO randomCards = availableCards[Random.Range(0, availableCards.Count)];

            if (!randomizeCards.Contains(randomCards))
            {
                randomizeCards.Add(randomCards);
            }
        }

        // Sinh ra 3 thẻ bài UI và đưa vào trong CardContainer
        cardOne = InstantiateCard(randomizeCards[0], cardContainer);
        cardTwo = InstantiateCard(randomizeCards[1], cardContainer);
        cardThree = InstantiateCard(randomizeCards[2], cardContainer);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform parentContainer)
    {
        GameObject cardGo = Instantiate(cardPrefab, parentContainer);
        Card card = cardGo.GetComponent<Card>();

        // Truyền thêm "this" vào hàm Setup để script Card biết nó thuộc về Manager nào
        card.Setup(cardSO, this);
        return cardGo;
    }

    // Hàm xử lý khi người chơi click chọn 1 trong 3 thẻ bài
    public void OnCardSelected(CardSO selectedVisualCard)
    {
        // 1. Thêm thẻ đã chọn vào danh sách nếu nó là thẻ Unique
        if (!alreadySelectedCards.Contains(selectedVisualCard))
        {
            alreadySelectedCards.Add(selectedVisualCard);
        }

        // 2. Làm biến mất ngay lập tức cả 3 thẻ bài bằng cách hủy (Destroy) chúng
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        // 3. Gọi ResourceManager (Singleton) để ẩn bảng UI và cho thời gian game chạy tiếp
        ResourceManager.Instance.ResumeGame();

        Debug.Log("Đã chọn thẻ: " + selectedVisualCard.cardText + " | Tiếp tục game.");
    }
}