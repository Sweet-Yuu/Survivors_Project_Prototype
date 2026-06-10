using System.Collections;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Player.IsDead = true;

        player.RB.linearVelocity = Vector2.zero;

        player.Anim.SetTrigger("die");

        
        player.InputHandler.enabled = false;
       

        GameManager.Instance.ClearAllEnemies();

        // ---------------------------------------------------------
        // HỆ THỐNG LƯU TRỮ KHI CHẾT
        // ---------------------------------------------------------
        if (MetaProgressManager.Instance != null)
        {
            // Giả sử lấy điểm từ PlayerExperience của run hiện tại để cộng vào
            // MetaProgressManager.Instance.AddRewardPoints(player.Experience.Điểm_Vừa_Kiếm_Được); 

            // Gọi Save khi màn hình Death Screen chuẩn bị xuất hiện
            MetaProgressManager.Instance.SaveData();
        }
        // ---------------------------------------------------------

        player.StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(3.017f);

        GameOverManager.Instance.GameOver();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
