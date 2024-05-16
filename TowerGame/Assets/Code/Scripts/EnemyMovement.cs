using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];

        // Set the starting rotation to look right
        transform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }

            RotateTowardsNextTarget();
        }
    }

    private void RotateTowardsNextTarget()
    {
        if (pathIndex < LevelManager.main.path.Length - 1)
        {
            Vector2 directionToNextTarget = (LevelManager.main.path[pathIndex].position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToNextTarget.y, directionToNextTarget.x) * Mathf.Rad2Deg;
            angle -= 90f; // Adjusting to match the sprite's orientation
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f); // Limit rotation to avoid flipping
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}