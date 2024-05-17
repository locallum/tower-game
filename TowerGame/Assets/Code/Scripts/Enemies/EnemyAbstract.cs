using UnityEngine;

public class AbstractEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int hitPoints;
    [SerializeField] protected int currencyWorth;

    protected Transform target;
    protected int pathIndex = 0;
    protected bool isDestroyed = false;

    protected virtual void Start()
    {
        target = LevelManager.main.path[pathIndex];
        transform.rotation = Quaternion.Euler(0f, 0f, -90f); // Set the starting rotation to look right
    }

    protected virtual void Update()
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

    protected virtual void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    protected virtual void RotateTowardsNextTarget()
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

    public virtual void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
