using System;
using UnityEngine;

public class ExplosionEnemy : Enemy
{
    [SerializeField] private GameObject explosionPrefabs;
    private void CreateExplosion()
    {
        if (explosionPrefabs != null)
        {
            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }
}