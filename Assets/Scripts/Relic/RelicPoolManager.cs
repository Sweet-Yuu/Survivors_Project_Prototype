// RelicPoolManager.cs
using System.Collections.Generic;
using UnityEngine;

public class RelicPoolManager : MonoBehaviour
{
    public static RelicPoolManager Instance { get; private set; }

    [Header("--- Khởi tạo dữ liệu (Initialization) ---")]
    
    [Tooltip("Master List chứa toàn bộ RelicData có trong project [cite: 19]")]
    public List<RelicData> masterRelicList = new List<RelicData>();

    // Danh sách sẽ liên tục thay đổi (bị trừ đi) trong suốt quá trình chơi [cite: 21]
    private List<RelicData> activeRandomPool = new List<RelicData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Tại thời điểm Start() của một Run mới [cite: 20]
        InitializePool();
    }

    public void InitializePool()
    {
        activeRandomPool.Clear();
        foreach (var relic in masterRelicList)
        {
            // Tự động lọc các Relic có category == RelicCategory.RandomPool và clone chúng sang danh sách runtime [cite: 20]
            if (relic.category == RelicCategory.RandomPool)
            {
                activeRandomPool.Add(relic);
            }
        }
        Debug.Log($"[RelicPoolManager] Đã khởi tạo Runtime Pool với {activeRandomPool.Count} relics ngẫu nhiên.");
    }

    // --- Public API (Các hàm giao tiếp cho Shop & Event gọi) [cite: 22] ---

    public RelicData GetRandomRelic()
    {
        // Kiểm tra activeRandomPool.Count [cite: 22]
        if (activeRandomPool.Count == 0)
        {
            // Nếu == 0, trả về Null (báo hiệu cạn kiệt) [cite: 24]
            Debug.LogWarning("[RelicPoolManager] Random Pool đã cạn kiệt! Yêu cầu Shop dùng Generic Consumable fallback.");
            return null;
        }

        // Nếu > 0, random một index và trả về Relic đó [cite: 23]
        int randomIndex = Random.Range(0, activeRandomPool.Count);
        return activeRandomPool[randomIndex];
    }

    public RelicData GetFixedRelic(string targetRelicID)
    {
        // Duyệt Master List để tìm Relic có ID trùng khớp và category == FixedEvent [cite: 25]
        foreach (var relic in masterRelicList)
        {
            if (relic.relicID == targetRelicID && relic.category == RelicCategory.FixedEvent)
            {
                return relic;
            }
        }
        Debug.LogError($"[RelicPoolManager] Không tìm thấy Fixed Relic với ID: {targetRelicID}");
        return null;
    }

    public void RemoveRelicFromPool(RelicData relicToRemove)
    {
        // Xóa Relic được truyền vào khỏi activeRandomPool [cite: 26]
        if (activeRandomPool.Contains(relicToRemove))
        {
            activeRandomPool.Remove(relicToRemove);
            Debug.Log($"[RelicPoolManager] Đã xóa {relicToRemove.ItemName} khỏi Pool. Còn lại: {activeRandomPool.Count}");
        }
    }

    public int GetActivePoolCount()
    {
        // Trả về số lượng Relic còn lại trong Pool 
        return activeRandomPool.Count;
    }
}