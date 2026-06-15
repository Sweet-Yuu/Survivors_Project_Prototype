using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [Header("UI Hiển Thị Tổng Điểm")]
    public TextMeshProUGUI txtTotalPoints;

    [Header("UI Của 1 Nút Nâng Cấp (Ví dụ: HP)")]
    public TextMeshProUGUI txtUpgradeName;
    public TextMeshProUGUI txtCurrentLevel;
    public TextMeshProUGUI txtCost;
    public Button btnUpgrade;

    [Header("Dữ liệu Nâng Cấp (Thẻ 2)")]
    public MetaUpgradeNode upgradeNode;

    private void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        // 1. Cập nhật tổng tiền góc màn hình
        if (MetaProgressManager.Instance != null)
        {
            txtTotalPoints.text = "Point: " + MetaProgressManager.Instance.totalRewardPoints.ToString();
        }

        // 2. Cập nhật thông tin của Nút nâng cấp
        if (upgradeNode != null)
        {
            txtUpgradeName.text = upgradeNode.upgradeName;

            // Lấy cấp độ hiện tại từ Manager Thẻ 3 (đã lưu dưới ổ cứng)
            int currentLevel = MetaProgressManager.Instance.GetUpgradeLevel(upgradeNode.upgradeName);

            if (currentLevel < upgradeNode.maxLevel)
            {
                // Chưa max level -> Hiện cấp độ và giá tiền cấp tiếp theo
                txtCurrentLevel.text = $"Level: {currentLevel}/{upgradeNode.maxLevel}";
                txtCost.text = $"Cost: {upgradeNode.costPerLevel[currentLevel]}";
                btnUpgrade.interactable = true; // Bật nút bấm
            }
            else
            {
                // Đã max level -> Khóa nút, ẩn giá tiền
                txtCurrentLevel.text = "Level: MAX";
                txtCost.text = "Cost: ---";
                btnUpgrade.interactable = false;
            }
        }
    }

    // Hàm này sẽ được gọi khi người chơi bấm nút (Sự kiện OnClick)
    public void OnUpgradeButtonClicked()
    {
        if (upgradeNode == null || MetaProgressManager.Instance == null) return;

        int currentLevel = MetaProgressManager.Instance.GetUpgradeLevel(upgradeNode.upgradeName);

        // Chặn lặp nếu đã max cấp
        if (currentLevel >= upgradeNode.maxLevel) return;

        // Lấy giá tiền của cấp độ hiện tại
        int cost = upgradeNode.costPerLevel[currentLevel];

        // Hỏi Manager Thẻ 3 xem có đủ tiền không
        if (MetaProgressManager.Instance.TrySpendPoints(cost))
        {
            // Trừ tiền thành công -> Tăng 1 cấp và lưu ngay vào ổ cứng
            MetaProgressManager.Instance.SaveUpgradeLevel(upgradeNode.upgradeName, currentLevel + 1);
            MetaProgressManager.Instance.SaveData(); // Lưu lại số tiền vừa bị trừ

            Debug.Log($"[Lobby UI] Purchased {upgradeNode.upgradeName} to level {currentLevel + 1}!");

            // Render lại chữ trên giao diện
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("[Lobby UI] Insufficient Reward Points!");
        }
    }
}