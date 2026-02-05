using UnityEngine;

public class VisionGizmo : MonoBehaviour
{
    [SerializeField]
    private float VisionRange;
    [SerializeField]
    private float PlayerDistance;

    private Transform player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        VisionRange = GetComponent<Animator>().GetBehaviour<IdleBehaviour>().VisionRange;
    }

    private void Update()
    {
        if (player != null) PlayerDistance = Vector3.Distance(transform.position, player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, VisionRange);
        Gizmos.color = Color.yellow;
        if (player != null) Gizmos.DrawLine(transform.position, player.position);
        Gizmos.color = Color.white;
    }
}
