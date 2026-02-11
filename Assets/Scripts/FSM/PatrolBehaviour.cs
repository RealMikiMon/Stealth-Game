using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float VisionRange;

    private EnemyController controller;
    private Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetBool("IsPatroling", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.PatrolMove();
        if (Vector2.Distance(animator.transform.position, player.position) < VisionRange)
        {
            animator.SetBool("IsChasing", true);
        }
        else
        {
            animator.SetBool("IsChasing", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsPatroling", false);
    }
}
