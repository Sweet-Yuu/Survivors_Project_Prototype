using UnityEngine;

public class BowAttack : IWeaponStrategy
{
   private Bow bow;
    private GameObject arrowPrefab;
    private Transform firePoint;

    private float lastAttackTime;
    public bool CanAttack => Time.time >= lastAttackTime + (2.0f/bow.BowData.attackSpeed);

    public BowAttack(Bow bow, GameObject arrowPrefab, Transform firePoint)
    {
        this.bow = bow;
        this.arrowPrefab = arrowPrefab;
        this.firePoint = firePoint;
    }
    public void ResetCooldown()
    {
        lastAttackTime = Time.time;
    }

    public void StartAttack()
    {
       
        if (arrowPrefab != null && firePoint != null)
        {
            GameObject arrow = Object.Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            if (arrow.TryGetComponent<Rigidbody2D>(out var rb2d))
            {
                rb2d.linearVelocity = firePoint.right * 15f;
            }
        }
    }

    public void UpdateAttack()
    {
       
    }

    public void EndAttack()
    {
        Debug.Log("Done");
    }
}
