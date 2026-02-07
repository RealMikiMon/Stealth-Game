using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsGround;
    public float Speed;
    public float distance = 0.5f;

    void Update()
    {
        Move();

        if (WallDetected()) Flip();
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    private bool WallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.right,distance,WhatIsGround);

        return hit.collider != null;
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}