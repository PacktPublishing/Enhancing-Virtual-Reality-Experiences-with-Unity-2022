using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDone : MonoBehaviour
{
    [Header("Stats")]
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}