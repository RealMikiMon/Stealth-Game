using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    Rigidbody2D rigidbody;
    private bool isMoving;

    public int Score = 2000;

    public bool IsMoving => isMoving;

    private Vector2 lastPosition;
    public float DistanceTraveled;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastPosition = rigidbody.position;
    }

    public void OnMove(InputValue value)
    {
        var moveDir = value.Get<Vector2>();

        Vector2 velocity = moveDir * speed;
        rigidbody.linearVelocity = velocity;

        isMoving = (velocity.magnitude > 0.01f);

        if (isMoving)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
        }
    }

    public void OnSaveScore()
    {
        PlayerPrefs.SetInt("Score", Score);
        Score = PlayerPrefs.GetInt("Score");
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 currentPosition = rigidbody.position;
            DistanceTraveled += Vector2.Distance(lastPosition, currentPosition);
            lastPosition = currentPosition;
        }
    }
}
