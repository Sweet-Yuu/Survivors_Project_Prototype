using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMoveSpeed = 3f;
    [Tooltip("Tích vào đây nếu hình gốc của quái vật đang quay mặt sang phải")]
    [SerializeField] protected bool isFacingRightByDefault = false;
    protected PlayerMovement player;
    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] protected float enterDamage = 10f;
    [SerializeField] protected float stayDamage = 1f;
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        currentHp = maxHp;
        UpdateHpBar();
    }
    protected virtual void Update()
    {
        MoveToPlayer();

    }
    protected void MoveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
    //protected void FlipEnemy()
    //{
    //    if (player != null)
    //    {
    //        transform.localScale = new Vector2(player.transform.position.x < transform.position.x ? 1 : -1, transform.localScale.y);
    //    }
    //}
    protected void FlipEnemy()
    {
        if (player != null)
        {
            // Xác định hướng: 1 (quay trái), -1 (quay phải) dựa trên vị trí Player
            float scaleX = player.transform.position.x < transform.position.x ? 1 : -1;

            // Nếu hình gốc vốn dĩ quay sang phải, ta phải lật ngược logic lại
            if (isFacingRightByDefault)
            {
                scaleX = scaleX * -1;
            }

            transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
}