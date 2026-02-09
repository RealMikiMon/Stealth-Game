using System.Collections.Generic;
using UnityEngine;

public class VisionDetector : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;

    [Header("Vision Settings")]
    public float DetectionRange = 3f;
    public float VisionAngle = 90f;

    private void OnDrawGizmos()
    {
        Vector2 forward = transform.right;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;

        var dir1 = Quaternion.AngleAxis(VisionAngle / 2f, Vector3.forward) * forward;
        Gizmos.DrawRay(transform.position, dir1 * DetectionRange);

        var dir2 = Quaternion.AngleAxis(-VisionAngle / 2f, Vector3.forward) * forward;
        Gizmos.DrawRay(transform.position, dir2 * DetectionRange);

        Gizmos.color = Color.white;
    }

    private void Update()
    {
        bool playerDetected = DetectPlayers().Length > 0;
        GetComponent<Animator>().SetBool("IsChasing", playerDetected);

        if (playerDetected)
            Debug.Log("Player DETECTED!");
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

    private bool PlayerInRange(ref List<Transform> players)
    {
        Collider2D[] playerColliders =
            Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);

        foreach (var item in playerColliders)
            players.Add(item.transform);

        return players.Count > 0;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        Vector2 forward = transform.right;

        for (int i = players.Count - 1; i >= 0; i--)
        {
            Vector2 targetDir = players[i].position - transform.position;
            float angle = Vector2.Angle(forward, targetDir);

            if (angle > VisionAngle / 2f)
                players.RemoveAt(i);
        }

        return players.Count > 0;
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            if (!IsVisible(players[i]))
                players.RemoveAt(i);
        }

        return players.Count > 0;
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dir,
            DetectionRange,
            WhatIsVisible | WhatIsPlayer
        );

        if (!hit.collider)
            return false;

        return hit.collider.transform == target;
    }
}

