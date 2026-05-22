using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashDuration = 0.7f;
    public float dashSpeed = 10f;
    public float dashCooldown = 1f;

    public float dashStartTime;
    public Vector2 dashDirection;
    public float lastDashTime=-999;

    public bool CanDash => Time.time >= lastDashTime + dashCooldown;
}
