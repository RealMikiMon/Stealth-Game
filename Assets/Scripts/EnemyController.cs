using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float Speed = 2f;
    public float ArriveDistance = 0.1f;
    private Transform currentTarget;

    private void Start()
    {
        currentTarget = PointB;
    }

    public void PatrolMove()
    {
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        transform.Translate(direction * Speed * Time.deltaTime, Space.World);

        if (ReachedTarget())
        {
            SwitchTarget();
            Flip();
        }
    }

    private bool ReachedTarget()
    {
        return Vector2.Distance(transform.position, currentTarget.position) < ArriveDistance;
    }

    private void SwitchTarget()
    {
        currentTarget = (currentTarget == PointA) ? PointB : PointA;
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
    }
}
