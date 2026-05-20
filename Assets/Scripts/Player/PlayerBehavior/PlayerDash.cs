using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashStartTime;
    public Vector2 dashDirection;
    public float lastDashTime=-999;

    public bool CanDash => Time.time >= lastDashTime + Player.Instance.PlayerData.dashCooldown;
}
