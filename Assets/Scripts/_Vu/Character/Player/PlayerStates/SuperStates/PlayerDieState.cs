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
        // BẮT ĐẦU PHẦN CODE BỔ SUNG CHO META PROGRESSION TẠI ĐÂY
        // ---------------------------------------------------------

        // 1. Gọi MetaProgressionManager để lưu trữ lại số "Điểm thưởng Data" mà người chơi vừa nhặt được trong run này.
        if (MetaProgressionManager.Instance != null)
        {
            // Giả sử lấy điểm từ PlayerExperience hoặc kho tạm nào đó
            // MetaProgressionManager.Instance.AddDataPoints(player.Experience.currentExp);
            MetaProgressionManager.Instance.SaveData();
        }

        // 2. Tương tác với chỉ số (Nếu muốn test trực tiếp không qua UI)
        if (player.playerStats is PlayerMetaStats metaStats)
        {
            // metaStats.MetaUpgradeData.dmgLevel++;
            // metaStats.MetaUpgradeData.SaveUpgrades(); 
        }

        // ---------------------------------------------------------
        // KẾT THÚC PHẦN CODE BỔ SUNG
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
