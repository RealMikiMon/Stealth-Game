using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisionDetector : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;

    [Header("Vision Settings")]
    public float DetectionRange = 3f;
    public float VisionAngle = 90f;

    [SerializeField] private float restartDistance;

    private LineRenderer lr;
    public int segments = 20;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();

            lr.positionCount = segments + 3;

            lr.useWorldSpace = true;
            lr.startWidth = 0.03f;
            lr.endWidth = 0.03f;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = new Color(1f, 1f, 0f, 0.4f);
            lr.endColor = new Color(1f, 1f, 0f, 0.4f);
        }
    }

    private void Update()
    {
        bool playerDetected = DetectPlayers().Length > 0;
        GetComponent<Animator>().SetBool("IsChasing", playerDetected);

        if (playerDetected)
        {
            SetConeColor(new Color(1f,0f,0f,0.4f));
        }
        else
        {
            SetConeColor(new Color(1f, 1f, 0f, 0.4f));
        }

        DrawVisionCone();
    }

    private void DrawVisionCone()
    {
        if (lr == null) return;

        Vector3 startPos = transform.position;
        Vector3 forward = transform.right;

        float halfAngle = VisionAngle / 2f;

        lr.SetPosition(0, startPos);

        for (int i = 0; i <= segments; i++)
        {
            float angle = -halfAngle + (VisionAngle / segments) * i;
            Vector3 dir = Quaternion.Euler(0, 0, angle) * forward;
            lr.SetPosition(i + 1, startPos + dir * DetectionRange);
        }

        lr.SetPosition(segments + 2, startPos);
    }

    private Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                if (PlayerIsVisible(ref players))
                {
                    CheckCloseDistance(players);
                }
            }
        }

        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);

        foreach (var item in playerColliders)
        {
            players.Add(item.transform);
        }
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
            {
                players.RemoveAt(i);
            }
        }

        return players.Count > 0;
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            if (!IsVisible(players[i]))
            {
                players.RemoveAt(i);
            }
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

    private void CheckCloseDistance(List<Transform> players)
    {
        foreach (Transform player in players)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= restartDistance)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    private void SetConeColor(Color c)
    {
        lr.startColor = c;
        lr.endColor = c;
    }
}