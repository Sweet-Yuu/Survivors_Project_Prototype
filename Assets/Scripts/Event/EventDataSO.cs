using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventChoice
{
    public string acionText; // Văn bản hiển thị cho lựa chọn[cite: 17]
    public string outcomeText; // Văn bản mô tả kết quả của lựa chọn[cite: 17]

    [SerializeReference] public List<IEventRewardStrategy> rewards = new List<IEventRewardStrategy>(); //[cite: 17]
}

[CreateAssetMenu(fileName = "NewStoryEvent", menuName = "Game Data/Event/Story Event")]
public class EventDataSO : ScriptableObject
{
    public string eventID; //[cite: 17]
    public string eventTitle; // Tiêu đề của sự kiện[cite: 17]
    [TextArea(5, 10)] public string eventLore; //[cite: 17]

    // === CHỖ SỬA: Thêm dòng này để Game Designer kéo thả ảnh minh họa ===
    public Sprite eventIllustration;

    public List<EventChoice> choices = new List<EventChoice>(); //[cite: 17]
}