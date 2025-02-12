using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;
    public float VisionAngle;

    private void OnDrawGizmos()
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

    private void Update()
    {
        if (DetectPlayers().Length > DetectionRange) Debug.Log("Player detected");
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
    private Transform[] DetectPlayers()
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
}
