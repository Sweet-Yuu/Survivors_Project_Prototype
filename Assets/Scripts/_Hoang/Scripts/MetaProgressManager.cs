using UnityEngine;

public class MetaProgressManager : MonoBehaviour
{
    // Cấu trúc Singleton để gọi từ mọi nơi
    public static MetaProgressManager Instance { get; private set; }

    [Header("Quỹ Điểm Thưởng")]
    public int totalRewardPoints;

    private void Awake()
    {
        // Đảm bảo chỉ có 1 Manager tồn tại xuyên suốt các màn chơi
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); // Tải dữ liệu ngay khi khởi động
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hàm cộng tiền vào quỹ
    public void AddRewardPoints(int points)
    {
        totalRewardPoints += points;
    }

    // Hàm kiểm tra và trừ tiền khi mua nâng cấp
    public bool TrySpendPoints(int cost)
    {
        if (totalRewardPoints >= cost)
        {
            totalRewardPoints -= cost;
            return true;
        }
        return false;
    }

    // ----------------------------------------------------
    // HỆ THỐNG SAVE / LOAD (PlayerPrefs)
    // ----------------------------------------------------

    public void SaveData()
    {
        // 1. Lưu tổng điểm thưởng
        PlayerPrefs.SetInt("TotalRewardPoints", totalRewardPoints);
        PlayerPrefs.Save();

        Debug.Log($"[SaveSystem] Đã lưu Game! Tổng điểm: {totalRewardPoints}");
    }

    private void LoadData()
    {
        // Tải tổng điểm thưởng (mặc định là 0 nếu chưa có file save)
        totalRewardPoints = PlayerPrefs.GetInt("TotalRewardPoints", 0);
    }

    // Hàm hỗ trợ: Lấy cấp độ hiện tại của một nút nâng cấp (Dành cho Thẻ 2 & 4)
    public int GetUpgradeLevel(string upgradeName)
    {
        return PlayerPrefs.GetInt("Upgrade_" + upgradeName, 0);
    }

    // Hàm hỗ trợ: Lưu cấp độ mới của một nút nâng cấp
    public void SaveUpgradeLevel(string upgradeName, int level)
    {
        PlayerPrefs.SetInt("Upgrade_" + upgradeName, level);
        PlayerPrefs.Save();
    }
}