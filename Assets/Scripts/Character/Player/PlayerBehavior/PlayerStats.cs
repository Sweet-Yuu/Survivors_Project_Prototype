using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentExp;

    public void AddExp(int amount)
    {
        currentExp += amount;

        Debug.Log("EXP: " + currentExp);
    }
}