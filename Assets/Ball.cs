using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    
    [Header("Ball Settings")]
    public bool freezeRotation = true; //if true, freeze rotation of ball
    
    [Header("Speed System")]
    public float baseSpeed; // Set by BallManager when spawned
    private float currentSpeedMultiplier = 1f; // For stacking modifiers

    private BallManager ballManager;
    public StateController stateController;

    void Start()
    {
        // Find BallManager reference
        ballManager = FindFirstObjectByType<BallManager>();
        stateController = FindFirstObjectByType<StateController>();
        
        if (freezeRotation)
        {
            rb.freezeRotation = true;
        }
        
        // BallManager will handle setting velocity, not the Ball itself
    }

    void Update()
    {
        // Keep trying to find BallManager if we don't have it
        if (ballManager == null) { ballManager = FindFirstObjectByType<BallManager>(); }
        if (stateController == null){stateController = FindFirstObjectByType<StateController>();}
    }
    
    public void Initialize(float speed, Vector2 direction)
    {
        baseSpeed = speed;
        currentSpeedMultiplier = 1f;
        rb.linearVelocity = direction.normalized * (baseSpeed * currentSpeedMultiplier);
    }
    
    public void SetSpeed(float newSpeed)
    {
        baseSpeed = newSpeed;
        UpdateVelocity();
    }
    
    public void ModifySpeed(float multiplier)
    {
        currentSpeedMultiplier *= multiplier;
        UpdateVelocity();
    }
    
    private void UpdateVelocity()
    {
        Vector2 currentDirection = rb.linearVelocity.normalized;
        rb.linearVelocity = currentDirection * (baseSpeed * currentSpeedMultiplier);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ballManager == null) return;

        if (other.CompareTag("AIGoal"))
        {
            // Player scored - ball hit AI's goal
            ballManager.OnBallScored(gameObject, false); // false = player scored
        }
        else if (other.CompareTag("PlayerGoal"))
        {
            // AI scored - ball hit player's goal
            ballManager.OnBallScored(gameObject, true); // true = AI scored
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("AP1") || collision.gameObject.name.Equals("UP1"))
        {
            //float theta = Vector2.SignedAngle(collision.gameObject.transform.position, gameObject.transform.position);
            //rb.linearVelocity = new Vector2(rb.linearVelocity.magnitude * math.cos(theta),rb.linearVelocity.magnitude * math.sin(theta)) * -1;
            rb.linearVelocity *= -1;
        }
        stateController.PlayerScored(10, gameObject.transform.position);
    }
}
