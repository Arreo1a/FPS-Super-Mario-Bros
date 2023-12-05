using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    [SerializeField] private int _health = 50;
    [SerializeField] private GameObject _destroyParticles;

    public void TakeDamage(int amount) {
        _health -= amount;

        if (_health <= 0 ) {
            Die();
        }
    }

    void Die() {
        Debug.Log("Death");
        _destroyParticles.SetActive(true);
        Destroy(gameObject);
    }
}
