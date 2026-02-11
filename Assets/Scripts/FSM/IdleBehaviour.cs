using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public float StayTime = 3f;
    public float VisionRange = 5f;
    public float ScanAngle = 90f;       
    public float ScanSpeed = 1.5f;      
    public float EdgePause = 0.4f;      
    public float SmoothFactor = 2f;     
    private float timer;
    private float scanTimer;
    private float pauseTimer;
    private bool pausing;
    private Transform player;
    private Quaternion startRotation;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        scanTimer = 0f;
        pauseTimer = 0f;
        pausing = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startRotation = animator.transform.rotation;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Scan(animator.transform);
        bool playerClose = IsPlayerClose(animator.transform);
        bool timeUp = IsTimeUp();
        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", timeUp);
    }

    private void Scan(Transform transform)
    {
        if (pausing)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= EdgePause)
            {
                pausing = false;
                pauseTimer = 0f;
            }
            return;
        }
        scanTimer += Time.deltaTime * ScanSpeed;
        float t = Mathf.PingPong(scanTimer, 1f);
        if (t < 0.02f || t > 0.98f)
        {
            pausing = true;
        }
        float smoothT = Mathf.SmoothStep(0f, 1f, t);
        float angle = Mathf.Lerp(-ScanAngle * 0.5f, ScanAngle * 0.5f, smoothT);
        transform.rotation = startRotation * Quaternion.Euler(0f, 0f, angle);
    }

    private bool IsTimeUp()
    {
        timer += Time.deltaTime;
        return timer > StayTime;
    }

    private bool IsPlayerClose(Transform transform)
    {
        float dist = Vector2.Distance(transform.position, player.position);
        return dist < VisionRange;
    }
}
