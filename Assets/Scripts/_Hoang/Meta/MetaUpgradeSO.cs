using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTreeData", menuName = "Data/Meta Progression/Skill Tree")]
public class MetaUpgradeSO : ScriptableObject
{
    [Header("All Skill Nodes")]
    public List<SkillNode> nodeList = new List<SkillNode>();

    // Dictionary dùng để tra cứu nhanh bằng ID trong lúc chơi
    private Dictionary<string, SkillNode> nodeDictionary;

    public void Initialize()
    {
        nodeDictionary = new Dictionary<string, SkillNode>();
        foreach (var node in nodeList)
        {
            if (!nodeDictionary.ContainsKey(node.nodeID))
            {
                nodeDictionary.Add(node.nodeID, node);
            }
        }
    }

    // Giao tiếp với UI: Thử mua một kỹ năng
    public bool TryUnlockNode(string nodeID, ref int currentDataPoints)
    {
        if (nodeDictionary == null) Initialize();

        if (nodeDictionary.TryGetValue(nodeID, out SkillNode node))
        {
            // 1. Kiểm tra ràng buộc Skill Tree
            if (!node.CanBeUnlocked(nodeDictionary))
            {
                Debug.Log($"[SkillTree] Chưa đủ điều kiện mở khóa {node.nodeName}!");
                return false;
            }

            // 2. Kiểm tra tiền
            if (currentDataPoints >= node.dataPointCost)
            {
                currentDataPoints -= node.dataPointCost;
                node.isUnlocked = true;
                SaveSkillTree();
                return true;
            }
            else
            {
                Debug.Log($"[SkillTree] Không đủ Data Points để mở {node.nodeName}!");
                return false;
            }
        }
        return false;
    }

    // Trả về tổng chỉ số cộng thêm của một loại Stat để đưa vào PlayerMetaStats
    public float GetTotalBonus(StatType type)
    {
        float total = 0f;
        foreach (var node in nodeList)
        {
            if (node.isUnlocked && node.statToUpgrade == type)
            {
                total += node.upgradeValue;
            }
        }
        return total;
    }

    public void SaveSkillTree()
    {
        foreach (var node in nodeList)
        {
            // Lưu trạng thái của từng Node bằng ID của nó
            PlayerPrefs.SetInt($"SkillNode_{node.nodeID}", node.isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadSkillTree()
    {
        foreach (var node in nodeList)
        {
            node.isUnlocked = PlayerPrefs.GetInt($"SkillNode_{node.nodeID}", 0) == 1;
        }
        Initialize(); // Dựng lại Dictionary sau khi Load
    }
}