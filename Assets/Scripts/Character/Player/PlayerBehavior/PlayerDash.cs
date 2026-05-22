using System;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashStartTime;
    public Vector2 dashDirection;
    public float lastDashTime=-999;


    public event Action OnDashEvent;


    public bool CanDash => Time.time >= lastDashTime + Player.Instance.PlayerData.dashCooldown;

    public void TriggerDashEvent()
    {
        OnDashEvent?.Invoke();
    }
}
