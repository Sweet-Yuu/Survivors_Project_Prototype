using UnityEngine;

public class SupportWeaponCooldownState : SupportWeaponState
{
    private float cooldownTimer;

    public SupportWeaponCooldownState(SupportWeaponController controller, SupportWeaponStateMachine stateMachine, SupportWeaponDataSO weaponData)
        : base(controller, stateMachine, weaponData) { }

    public override void Enter()
    {
        base.Enter();

        // Tính toán tổng thời gian hồi (Fire Rate + thời gian xả chuỗi đạn)
        float burstDuration = (weaponData.projectileCount - 1) * 0.1f;
        cooldownTimer = weaponData.fireRate + burstDuration;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        cooldownTimer -= Time.deltaTime;

        // Hết thời gian hồi chiêu -> Quay lại tìm quái
        if (cooldownTimer <= 0f)
        {
            stateMachine.ChangeState(controller.SearchState);
        }
    }
}