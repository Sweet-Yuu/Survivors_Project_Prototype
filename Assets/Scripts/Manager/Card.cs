using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] Image cardImageRenderer;
    [SerializeField] TextMeshProUGUI cardTextRenderer;

    private CardSO cardInfo;
    private CardManager manager;
    public void Setup(CardSO card, CardManager cardManager)
    {
        cardInfo = card;
        manager = cardManager;
        cardImageRenderer.sprite = card.cardImage;
        cardTextRenderer.text = card.cardText;
    }

    public void OnClickCard()
    {
        if (manager != null)
        {
            // [Mở rộng sau này]: Bạn có thể viết thêm logic kích hoạt chỉ số ở đây
            // Ví dụ: PlayerStats.ApplyEffect(cardInfo.effectType, cardInfo.effectValue);

            // Báo cho CardManager biết thẻ này đã được chọn để thực hiện xóa cả 3 thẻ bài
            manager.OnCardSelected(cardInfo);
        }
    }
}
