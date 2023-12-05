using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 30;

    public void TakeDamage(int amount) {
        _health -= amount;

        if (_health <= 0 ) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}