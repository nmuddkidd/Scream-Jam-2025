using UnityEngine;

public class A1 : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float reactionDelay = 0.2f;
    
    private Ball ballScript;
    private float lastReactionTime;
    private Vector2 targetY;
    private bool canReact = true;
    private Vector2 lastBallVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballScript = FindFirstObjectByType<Ball>();
        lastReactionTime = Time.time;
        targetY = transform.position; // Initialize target
    }

    // Update is called once per frame
    void Update()
    {
        if (ballScript == null) return;

        Vector2 ballPosition = ballScript.transform.position;
        Vector2 ballVelocity = ballScript.rb.linearVelocity;

        // Check if ball direction changed (bounced off wall)
        if (lastBallVelocity != Vector2.zero && 
            Vector2.Dot(ballVelocity.normalized, lastBallVelocity.normalized) < 0.5f)
        {
            canReact = true;
            lastReactionTime = Time.time;
        }

        // Only react after delay period
        if (Time.time - lastReactionTime >= reactionDelay && canReact)
        {
            // Avoid division by zero
            if (Mathf.Abs(ballVelocity.x) > 0.1f)
            {
                // Predict where ball will be
                float timeToReach = Mathf.Abs(ballPosition.x - transform.position.x) / Mathf.Abs(ballVelocity.x);
                float predictedY = ballPosition.y + (ballVelocity.y * timeToReach);
                targetY = new Vector2(transform.position.x, predictedY);
            }
            else
            {
                // If ball isn't moving horizontally much, just track current position
                targetY = new Vector2(transform.position.x, ballPosition.y);
            }
            
            lastReactionTime = Time.time;
            canReact = false;
        }

        // Move towards target
        Vector2 currentPos = transform.position;
        float direction = Mathf.Sign(targetY.y - currentPos.y);
        
        if (Mathf.Abs(targetY.y - currentPos.y) > 0.1f)
        {
            transform.Translate(Vector2.up * direction * moveSpeed * Time.deltaTime);
        }

        // Store velocity for next frame comparison
        lastBallVelocity = ballVelocity;

        // Also allow reaction when ball is moving toward AI paddle
        if (ballVelocity.x * Mathf.Sign(transform.position.x) > 0)
        {
            canReact = true;
        }
    }
}
