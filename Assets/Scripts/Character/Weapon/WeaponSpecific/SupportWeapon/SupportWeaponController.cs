using UnityEngine;

public class SupportWeaponController : MonoBehaviour
{
    [Header("Weapon Configuration")]
    [field: SerializeField] public SupportWeaponDataSO WeaponData { get; private set; }
    [field: SerializeField] public Transform FirePoint { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

    // --- Khai báo State Machine ---
    public SupportWeaponStateMachine StateMachine { get; private set; }
    public SupportWeaponSearchState SearchState { get; private set; }
    public SupportWeaponCooldownState CooldownState { get; private set; }

    public IWeaponAttackStrategy AttackStrategy { get; private set; }

    private void Awake()
    {
        AttackStrategy = GetComponent<IWeaponAttackStrategy>();

        // Khởi tạo các State
        StateMachine = new SupportWeaponStateMachine();
        SearchState = new SupportWeaponSearchState(this, StateMachine, WeaponData);
        CooldownState = new SupportWeaponCooldownState(this, StateMachine, WeaponData);
    }

    private void Start()
    {
        // Bắt đầu vào game là đưa vào trạng thái tìm kiếm
        if (WeaponData != null)
        {
            StateMachine.Initialize(SearchState);
        }
    }

    private void Update()
    {
        if (WeaponData == null || AttackStrategy == null) return;

        // Chỉ cần 1 dòng duy nhất để chạy State Machine
        StateMachine.CurrentState.LogicUpdate();
    }

    // Hàm quét quái được thiết lập Public để SearchState có thể sử dụng
    public Transform FindNearestEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, WeaponData.attackRange, EnemyLayer);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                float distance = Vector2.Distance(transform.position, enemyCollider.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemyCollider.transform;
                }
            }
        }
        return nearestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        if (WeaponData != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, WeaponData.attackRange);
        }
    }
}