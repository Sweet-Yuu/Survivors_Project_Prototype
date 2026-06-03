using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [Header("--- Event Data Pools ---")]
    [SerializeField] private List<EventDataSO> storyEventPool = new List<EventDataSO>(); // Bỏ các file cốt truyện SO vào đây

    [Header("--- UI References ---")]
    [SerializeField] private GameObject eventCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI titleText; // Kéo chữ TitleText vào đây
    [SerializeField] private TMPro.TextMeshProUGUI loreText;  // Kéo chữ LoreText vào đây
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    [Header("--- Cấu hình Scene ---")]
    [SerializeField] private string nextLevelSceneName = "Wave_2"; // Tên scene sau khi xong sự kiện

    private List<GameObject> spawnedButtons = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenRandomEvent()
    {
        Time.timeScale = 0f; // Tạm dừng game
        eventCanvas.SetActive(true);

        // Nếu hết sự kiện thì cho qua màn luôn
        if (storyEventPool.Count == 0)
        {
            ExitEventAndLoadScene(nextLevelSceneName);
            return;
        }

        // Bốc ngẫu nhiên 1 sự kiện cốt truyện
        int randomIndex = Random.Range(0, storyEventPool.Count);
        EventDataSO selectedEvent = storyEventPool[randomIndex];

        // Đổ văn bản cốt truyện ra màn hình UI
        if (titleText != null) titleText.text = selectedEvent.eventTitle;
        if (loreText != null) loreText.text = selectedEvent.eventLore;

        // Xóa các nút cũ trước đó
        foreach (var button in spawnedButtons) Destroy(button);
        spawnedButtons.Clear();

        PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();

        // Sinh ra các nút bấm lựa chọn dựa vào danh sách rẽ nhánh của câu chuyện này
        foreach (var choice in selectedEvent.choices)
        {
            GameObject btnObj = Instantiate(choiceButtonPrefab, choicesContainer);
            spawnedButtons.Add(btnObj);

            var choiceUI = btnObj.GetComponent<EventChoiceButtonUI>();
            choiceUI.Setup(choice.acionText, choice.outcomeText);

            // Kiểm tra người chơi có đủ điều kiện ấn nút này không (Vd: cần 50 vàng)
            bool canSelect = true;
            foreach (var reward in choice.rewards)
            {
                if (playerStats != null && !reward.CanExecute(playerStats))
                {
                    canSelect = false;
                    break;
                }
            }

            var buttonComponent = btnObj.GetComponent<UnityEngine.UI.Button>();
            if (!canSelect)
            {
                buttonComponent.interactable = false;
                choiceUI.MarkAsDisabled(); // Đổi màu xám nếu không đủ điều kiện
            }
            else
            {
                buttonComponent.onClick.AddListener(() => OnChoiceSelected(choice, playerStats));
            }
        }
    }

    private void OnChoiceSelected(EventChoice choice, PlayerStats playerStats)
    {
        // Vô hiệu hóa ngay lập tức các nút khác để tránh Double-click
        foreach (var button in spawnedButtons)
        {
            button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        // Thực thi việc trao thưởng hoặc trừ phạt dựa vào lựa chọn
        foreach (var reward in choice.rewards)
        {
            reward.ExecuteReward(playerStats);
        }

        // Rời đi và qua màn chiến đấu tiếp theo
        ExitEventAndLoadScene(nextLevelSceneName);
    }

    private void ExitEventAndLoadScene(string sceneName)
    {
        eventCanvas.SetActive(false);
        Time.timeScale = 1f; // Khôi phục thời gian
        SceneManager.LoadScene(sceneName);
    }
}