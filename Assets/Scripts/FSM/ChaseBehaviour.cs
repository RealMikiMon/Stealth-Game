using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    public float Speed = 2;
    public float VisionRange;
    private Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("IsChasing", playerClose);
        Vector2 dir = player.position - animator.transform.position;
        animator.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, player.position);
        return (dist < VisionRange);
    }
}
