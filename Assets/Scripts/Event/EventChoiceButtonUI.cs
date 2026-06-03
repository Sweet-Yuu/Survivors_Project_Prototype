using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventChoiceButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAction;
    [SerializeField] private TextMeshProUGUI textOutcome;
    [SerializeField] private Image buttonImage;

    public void Setup(string action, string outcome)
    {
        if(textAction != null)
            textAction.text = action;
        if(textOutcome != null)
            textOutcome.text = outcome;
    }
    
    public void MarkAsDisabled()
    {
        if(buttonImage != null) buttonImage.color = Color.gray; // Đổi màu nút thành xám để biểu thị rằng nó đã bị vô hiệu hóa
        if(textAction != null) textAction.color = Color.darkGray; // Đổi màu văn bản hành động thành xám đậm
        if(textOutcome != null) textOutcome.color = Color.darkGray; // Đổi màu văn bản kết quả thành xám đậm
    }
        
}
