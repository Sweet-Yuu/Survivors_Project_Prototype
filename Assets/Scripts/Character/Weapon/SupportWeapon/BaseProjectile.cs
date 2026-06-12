using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseProjectile : MonoBehaviour
{
    protected IObjectPool<GameObject> pool;
    protected float damage;
    protected float lifeTimer;

    // Gắn thẻ quản lý Pool
    public void SetPool(IObjectPool<GameObject> poolReference)
    {
        pool = poolReference;
    }

    // Hàm Setup cơ bản, các class con sẽ override (ghi đè) để nạp thêm dữ liệu riêng
    public virtual void SetupBase(float weaponDamage, float lifeTime)
    {
        damage = weaponDamage;
        lifeTimer = lifeTime;
    }

    protected virtual void Update()
    {
        // Tự động đếm ngược thời gian sống và thu hồi
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    // Xử lý thu hồi an toàn để không gây lỗi Lifecycle
    public virtual void ReturnToPool()
    {
        if (gameObject.activeInHierarchy)
        {
            if (pool != null)
            {
                pool.Release(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}