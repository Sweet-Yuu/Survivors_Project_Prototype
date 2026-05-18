using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour
{
    private enum EnemyState { Idle, Chasing, Charging, Cooldown }
    [SerializeField] private EnemyState currentState = EnemyState.Idle;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float detectionRange = 7f;

    [Header("Charge/Attack Settings")]
    public float chargeRange = 4f;       // Khoảng cách bắt đầu kích hoạt cú lao
    public float chargeSpeed = 15f;      // Tốc độ lao đến đấm
    public float chargeDuration = 0.3f;   // Thời gian lao (giây)
    public float attackDamage = 20f;
    public float attackCooldown = 2f;    // Thời gian nghỉ sau khi đấm xong

    private Rigidbody2D rb;
    private Transform playerTransform;
    private float lastAttackTime;
    private Vector2 chargeDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Tìm Player trong Scene thông qua Instance bạn đã làm ở các câu trước
        if (Player.Instance != null)
        {
            playerTransform = Player.Instance.transform;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Logic chuyển đổi trạng thái bằng mắt (Cơ chế AI)
        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                // Nếu người chơi chạy quá xa thì lười không đuổi nữa
                if (distanceToPlayer > detectionRange)
                {
                    currentState = EnemyState.Idle;
                }
                // Nếu đủ gần và đã hồi chiêu xong -> LAO ĐẾN ĐẤM!
                else if (distanceToPlayer <= chargeRange && Time.time >= lastAttackTime + attackCooldown)
                {
                    StartCoroutine(ChargeAttackRoutine());
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        // Xử lý vận tốc vật lý dựa trên trạng thái
        if (currentState == EnemyState.Chasing && playerTransform != null)
        {
            Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else if (currentState == EnemyState.Charging)
        {
            // Duy trì vận tốc lao cực mạnh trong lúc đang Coroutine
            rb.linearVelocity = chargeDirection * chargeSpeed;
        }
        else if (currentState == EnemyState.Idle || currentState == EnemyState.Cooldown)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // Coroutine xử lý cú lao dứt khoát
    private IEnumerator ChargeAttackRoutine()
    {
        currentState = EnemyState.Charging;

        // Khóa hướng lao tại thời điểm nhắm vào Player
        chargeDirection = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;

        // Đợi hết thời gian lao đi
        yield return new WaitForSeconds(chargeDuration);

        // Lao xong thì chuyển sang trạng thái hồi sức (Cooldown)
        currentState = EnemyState.Cooldown;
        lastAttackTime = Time.time;

        yield return new WaitForSeconds(0.5f); // Khựng lại một chút sau cú đấm cho người chơi có cơ hội né
        currentState = EnemyState.Idle;
    }

    // XỬ LÝ VA CHẠM: Khi lao vào đè trúng người Player thì đấm gây dame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem có va chạm đúng vào Player không
        if (collision.gameObject.CompareTag("Player"))
        {
            // Thử lấy component IDamageable (PlayerHealth) trên Player để trừ máu
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null && currentState == EnemyState.Charging)
            {
                damageable.TakeDamage(attackDamage);

                // Đấm trúng rồi thì dừng cú lao lại ngay, tránh việc đẩy Player đi quá xa
                currentState = EnemyState.Cooldown;
            }
        }
    }

    // Vẽ vòng tròn giả lập trên giao diện Unity để bạn dễ căn chỉnh tầm đánh
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chargeRange);
    }
}