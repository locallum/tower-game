using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform turretRotatationPoint;
    [SerializeField] private LayerMask enemyMask;


    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;

    private Transform target;

    private void Update() {
        if (target == null) {
            FindTarget();
            return;
        }
        RotateTowardsTarget();
    }

    private void RotateTowardsTarget() {
        float angle = Mathf.Atan(target.position.y - transform.position.y, target.position.x - transform.postion.x) * Mathf.Rad2Deg + 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotatationPoint.rotation = targetRotation;
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if(hit.length > 0) {
            target = hits[0].transform;
        }
    }

    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;

        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

}
