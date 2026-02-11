using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public Transform EdgeDetectionPoint;
    public LayerMask WhatIsGround;
    public float Speed = 2f;
    public float ArriveDistance = 0.1f;
    public float WallCheckDistance = 0.5f;
    private Transform currentTarget;

    void Start()
    {
        currentTarget = PointB; 
    }

    void Update()
    {
        MoveTowardsTarget();
        if (ReachedTarget())
        {
            SwitchTarget();
            Flip();
        }
        if (WallDetected())
        {
            Flip();
            SwitchTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        transform.Translate(direction * Speed * Time.deltaTime, Space.World);
    }

    private bool ReachedTarget()
    {
        return Vector2.Distance(transform.position, currentTarget.position) < ArriveDistance;
    }

    private void SwitchTarget()
    {
        currentTarget = (currentTarget == PointA) ? PointB : PointA;
    }

    private bool WallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, WallCheckDistance, WhatIsGround);
        return hit.collider != null;
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
