using UnityEngine;

public class SupportWeaponSearchState : SupportWeaponState
{
    public SupportWeaponSearchState(SupportWeaponController controller, SupportWeaponStateMachine stateMachine, SupportWeaponDataSO weaponData)
        : base(controller, stateMachine, weaponData) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Tìm quái gần nhất
        Transform target = controller.FindNearestEnemy();

        if (target != null)
        {
            // Bắn đạn (gọi đến hàm Object Pooling ở Strategy)
            controller.AttackStrategy.ExecuteAttack(controller.FirePoint, target, weaponData);

            // Bắn xong thì lập tức chuyển sang trạng thái chờ hồi chiêu
            stateMachine.ChangeState(controller.CooldownState);
        }
    }
}