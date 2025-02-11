using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject patrolPointA;
    [SerializeField] private GameObject patrolPointB;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float MovemntSpeedPatrol;
    [Header("Enemy Detection")]
    [SerializeField] private float DetectionRange;
    public LayerMask WhatIsVisible;
    public LayerMask WhatIsPlayer;
    private Rigidbody2D rb;
    private Transform CurrentPoint;
    private GameObject Player;
    private Animator animator;
    public float VisionAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentPoint = patrolPointB.transform;

        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        bool result = false;
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);

        if (playerColliders.Length != 0)
        {
            result = true;

            foreach (var item in playerColliders)
            {
                players.Add(item.transform);
            }
        }

        return result;
    }
    private bool PlayerInAngle(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var angle = GetAngle(players[i]);

            if (angle > VisionAngle / 2)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private float GetAngle(Transform target)
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Vector2.Angle(targetDir, transform.right);

        return angle;
    }

    public Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                PlayerIsVisible(ref players);
            }
        }
        return players.ToArray();
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var isVisible = IsVisible(players[i]);

            if (!isVisible)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(
           transform.position,
           dir,
           DetectionRange,
           WhatIsVisible
        );

        return (hit.collider.transform == target);
    }

    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Jugador encontrado");
        }

        if (Vector2.Distance(transform.position, Player.transform.position) > DetectionRange)
        {
            Debug.Log("Jugador fuera de rango. Patrullando...");
            transform.position = Vector2.MoveTowards(transform.position, CurrentPoint.position, MovemntSpeedPatrol * Time.deltaTime);

            if (Vector2.Distance(transform.position, CurrentPoint.position) < 0.1f)
            {
                Flip(); 
                CurrentPoint = (CurrentPoint == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
            }
        }
        else
        {
            Transform[] detectedPlayers = DetectPlayers();

            if (detectedPlayers.Length > 0)
            {
                Debug.Log("Jugador detectado. Persecución iniciada.");
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MovementSpeed * Time.deltaTime);
                Debug.Log("Persiguiendo al jugador...");
            }
            else
            {
                Debug.Log("Jugador no detectado. Patrullando...");
                transform.position = Vector2.MoveTowards(transform.position, CurrentPoint.position, MovemntSpeedPatrol * Time.deltaTime);

                if (Vector2.Distance(transform.position, CurrentPoint.position) < 0.1f)
                {
                    Flip(); 
                    CurrentPoint = (CurrentPoint == patrolPointA.transform) ? patrolPointB.transform : patrolPointA.transform;
                }
            }
        }
    }


    /* private void FixedUpdate()
     {
         if (Player == null)
         {
             Player = GameObject.FindGameObjectWithTag("Player");
         }
         else if (DetectPlayers().Length > 0)
         {
             if (Vector2.Distance(transform.position, Player.transform.position) < DetectionRange)
             {
                 Debug.Log("persecucion");
                 transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MovementSpeed * Time.deltaTime);
             }
         }
         else
         {
             Debug.Log("patrol");
             transform.position = Vector2.MoveTowards(transform.position, CurrentPoint.position, MovemntSpeedPatrol * Time.deltaTime);
         }

     }*/

    private void Flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;
        var direction = Quaternion.AngleAxis(VisionAngle / 2, transform.forward)
            * transform.right;
        Gizmos.DrawRay(transform.position, direction * DetectionRange);
        var direction2 = Quaternion.AngleAxis(-VisionAngle / 2, transform.forward)
            * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(patrolPointA.transform.position, 0.2f);
        Gizmos.DrawWireSphere(patrolPointB.transform.position, 0.2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(patrolPointA.transform.position, patrolPointB.transform.position);
    }

#endif 
}