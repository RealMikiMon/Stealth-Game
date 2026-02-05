using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;

    private float timer;
    private Transform player;
    private Vector2 targetPos;
    private Vector2 startPos;

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startPos = new Vector2(animator.transform.position.x, animator.transform.position.y);
        targetPos = new Vector2(startPos.x + Random.Range(-1.0f, 1.0f) * 4, startPos.y + Random.Range(-1.0f, 1.0f) * 4);
    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", !timeUp);

        // Move
        animator.transform.position = Vector2.Lerp(startPos, targetPos, timer / StayTime);
    }

    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return (timer > StayTime);
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, player.position);
        return (dist < VisionRange);
    }
}
