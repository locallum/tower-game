using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Health : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 3;

    public void TakeDamage(int dmg) {
        hitPoints -= dmg;

        if (hitPoints <= 0) {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
