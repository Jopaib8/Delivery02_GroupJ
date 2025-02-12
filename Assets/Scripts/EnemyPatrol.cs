using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private GameObject patrolPointA;
    [SerializeField] private GameObject patrolPointB;
    [SerializeField] private float patrolSpeed = 2f;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float visionAngle = 90f;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsObstacle;

    private Rigidbody2D rb;
    private Transform currentPoint;
    private GameObject player;
    private bool isChasing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = patrolPointB.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null) return;
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            CheckForPlayer();
        }
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            Flip();
            currentPoint = (currentPoint == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange, whatIsPlayer);
        foreach (var playerCollider in playersInRange)
        {
            Transform playerTransform = playerCollider.transform;
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);
            if (angleToPlayer < visionAngle / 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, whatIsObstacle);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    isChasing = true;
                    break;
                }
            }
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) > detectionRange)
        {
            isChasing = false;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Vector3 forward = transform.right;
        Vector3 leftDir = Quaternion.AngleAxis(-visionAngle / 2, Vector3.forward) * forward;
        Vector3 rightDir = Quaternion.AngleAxis(visionAngle / 2, Vector3.forward) * forward;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * detectionRange);
    }
}