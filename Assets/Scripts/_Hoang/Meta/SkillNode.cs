using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillNode
{
    [Header("Node Info")]
    public string nodeID;           // Mã định danh duy nhất (VD: "HP_Tier1", "Dash_Tier2")
    public string nodeName;         // Tên hiển thị trên UI
    public int dataPointCost;       // Giá tiền để mở khóa
    public bool isUnlocked;         // Trạng thái mở khóa

    [Header("Upgrade Effect")]
    public StatType statToUpgrade;  // Tái sử dụng enum StatType bên PlayerStats.cs
    public float upgradeValue;      // Chỉ số cộng thêm (VD: +10 HP)

    [Header("Dependencies (Dây mơ rễ má)")]
    // Danh sách ID của các Node cần phải mở trước khi mở Node này
    // Ví dụ: Để mở "HP_Tier2", list này phải chứa chuỗi "HP_Tier1"
    public List<string> requiredNodeIDs = new List<string>();

    // Hàm kiểm tra xem Node này có thỏa mãn điều kiện để mở khóa chưa
    public bool CanBeUnlocked(Dictionary<string, SkillNode> allNodes)
    {
        if (isUnlocked) return false; // Đã mở rồi thì không mở lại

        // Duyệt qua tất cả các điều kiện rễ
        foreach (string reqID in requiredNodeIDs)
        {
            if (allNodes.TryGetValue(reqID, out SkillNode reqNode))
            {
                // Nếu có bất kỳ Node gốc nào chưa mở -> Node hiện tại chưa được phép mở
                if (!reqNode.isUnlocked) return false;
            }
            else
            {
                Debug.LogWarning($"[SkillTree] Không tìm thấy Node điều kiện: {reqID}");
                return false;
            }
        }
        return true;
    }
}