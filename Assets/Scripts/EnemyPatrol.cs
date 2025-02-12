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
        currentPoint = patrolPointB.transform; // Comienza moviéndose hacia el punto B
        player = GameObject.FindGameObjectWithTag("Player"); // Encuentra al jugador al inicio
    }

    private void Update()
    {
        if (player == null) return; // Si no hay jugador, no hacer nada

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
        // Mueve al enemigo hacia el punto actual de patrulla
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, patrolSpeed * Time.deltaTime);

        // Si el enemigo está cerca del punto actual, cambia al siguiente punto
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            Flip();
            currentPoint = (currentPoint == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
        }
    }

    private void CheckForPlayer()
    {
        // Verifica si el jugador está dentro del rango de detección
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange, whatIsPlayer);

        foreach (var playerCollider in playersInRange)
        {
            Transform playerTransform = playerCollider.transform;
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Verifica si el jugador está dentro del ángulo de visión
            float angleToPlayer = Vector2.Angle(transform.right, directionToPlayer);
            if (angleToPlayer < visionAngle / 2)
            {
                // Verifica si hay obstáculos entre el enemigo y el jugador
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, whatIsObstacle);

                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    isChasing = true; // Comienza a perseguir al jugador
                    break;
                }
            }
        }
    }

    private void ChasePlayer()
    {
        // Mueve al enemigo hacia la posición del jugador
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, patrolSpeed * Time.deltaTime);

        // Si el jugador se escapa del rango de detección, vuelve a patrullar
        if (Vector2.Distance(transform.position, player.transform.position) > detectionRange)
        {
            isChasing = false;
        }
    }

    private void Flip()
    {
        // Voltea al enemigo y ajusta la dirección del cono de visión
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Dibuja el cono de visión
        Vector3 forward = transform.right;
        Vector3 leftDir = Quaternion.AngleAxis(-visionAngle / 2, Vector3.forward) * forward;
        Vector3 rightDir = Quaternion.AngleAxis(visionAngle / 2, Vector3.forward) * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * detectionRange);
    }
}