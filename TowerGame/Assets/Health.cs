using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Health : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 3;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
