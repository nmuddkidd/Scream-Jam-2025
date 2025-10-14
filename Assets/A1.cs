using UnityEngine;

public class A1 : MonoBehaviour
{
    [Header("AI Settings")]
    public float moveSpeed = 4f;
    public float reactionDelay = 0.2f;
    
    [Header("Boundary Objects")]
    public GameObject topBoundary;
    public GameObject bottomBoundary;
    
    // Boundary limits
    private float topLimit;
    private float bottomLimit;
    private float paddleHalfHeight;
    
    // Ball tracking
    private Ball currentBall;
    private BallManager ballManager;
    private StateController stateController;
    
    // AI behavior
    private float lastReactionTime;
    private Vector2 targetY;
    private bool canReact = true;
    private Vector2 lastBallVelocity;
    private bool isEnabled = true;

    void Start()
    {
        // Find managers
        ballManager = FindFirstObjectByType<BallManager>();
        stateController = FindFirstObjectByType<StateController>();
        
        // Initialize AI
        lastReactionTime = Time.time;
        targetY = transform.position;
        
        // Set up boundaries
        UpdateBoundaries();
        
        //Debug.Log("A1: AI Paddle initialized");
    }
    
    void UpdateBoundaries()
    {
        // Get paddle half-height for collision detection
        Renderer paddleRenderer = GetComponent<Renderer>();
        if (paddleRenderer != null)
        {
            paddleHalfHeight = paddleRenderer.bounds.size.y / 2f;
        }
        
        // Get boundary positions
        if (topBoundary != null)
        {
            Renderer topRenderer = topBoundary.GetComponent<Renderer>();
            topLimit = topRenderer.bounds.min.y - paddleHalfHeight;
        }
        else
        {
            topLimit = 5f; // Default fallback
        }
        
        if (bottomBoundary != null)
        {
            Renderer bottomRenderer = bottomBoundary.GetComponent<Renderer>();
            bottomLimit = bottomRenderer.bounds.max.y + paddleHalfHeight;
        }
        else
        {
            bottomLimit = -5f; // Default fallback
        }
        
        //Debug.Log($"A1: Boundaries set - Top: {topLimit}, Bottom: {bottomLimit}");
    }

    void Update()
    {
        // Debug: Check all conditions
        //Debug.Log($"A1 Update: isEnabled={isEnabled}, stateController={stateController != null}, isPlaying={stateController?.IsGamePlaying()}");
        
        // Only move if enabled and game is playing
        if (!isEnabled || stateController == null || !stateController.IsGamePlaying())
        {
            Debug.Log("A1: Not moving - conditions not met");
            return;
        }
            
        // Find a ball to track
        UpdateBallReference();
        
        if (currentBall == null) 
        {
            Debug.Log("A1: No ball found to track");
            return;
        }

        //Debug.Log($"A1: Tracking ball at {currentBall.transform.position}");

        Vector2 ballPosition = currentBall.transform.position;
        Vector2 ballVelocity = currentBall.rb.linearVelocity;

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
                
                // Clamp predicted position within boundaries
                predictedY = Mathf.Clamp(predictedY, bottomLimit, topLimit);
                targetY = new Vector2(transform.position.x, predictedY);
            }
            else
            {
                // If ball isn't moving horizontally much, just track current position
                float clampedY = Mathf.Clamp(ballPosition.y, bottomLimit, topLimit);
                targetY = new Vector2(transform.position.x, clampedY);
            }
            
            lastReactionTime = Time.time;
            canReact = false;
        }

        // Move towards target with boundary constraints
        Vector2 currentPos = transform.position;
        float direction = Mathf.Sign(targetY.y - currentPos.y);
        
        if (Mathf.Abs(targetY.y - currentPos.y) > 0.1f)
        {
            Vector3 newPosition = currentPos + (Vector2.up * direction * moveSpeed * Time.deltaTime);
            
            // Clamp final position within boundaries
            newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit, topLimit);
            
            transform.position = newPosition;
        }

        // Store velocity for next frame comparison
        lastBallVelocity = ballVelocity;

        // Also allow reaction when ball is moving toward AI paddle
        if (ballVelocity.x * Mathf.Sign(transform.position.x) > 0)
        {
            canReact = true;
        }
    }
    
    void UpdateBallReference()
    {
        // If we don't have a ball or it's been destroyed, find a new one
        /*if (currentBall == null)
        {
            //Debug.Log("A1: Looking for ball...");
            if (ballManager != null && ballManager.HasActiveBalls())
            {
                GameObject ballObj = ballManager.GetClosestBall(transform.position.x);
                if (ballObj != null)
                {
                    currentBall = ballObj.GetComponent<Ball>();
                    //Debug.Log("A1: Found ball to track");
                }
                else
                {
                    //Debug.Log("A1: BallManager has balls but GetFirstBall() returned null");
                }
            }
            else
            {
                //Debug.Log($"A1: BallManager null or no active balls. BallManager={ballManager != null}, HasBalls={ballManager?.HasActiveBalls()}");
            }
        }*/

        //AI tracks Closest ball each frame (OPTOMIZE THIS!)
        currentBall = ballManager.GetClosestBall(transform.position.x).GetComponent<Ball>();

        // Double-check that our ball reference is still valid
        if (currentBall != null && currentBall.gameObject == null)
        {
            //Debug.Log("A1: Ball reference became invalid, clearing");
            currentBall = null;
        }
    }
    
    // Called by StateController
    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
        //Debug.Log($"A1: AI Paddle {(enabled ? "enabled" : "disabled")}");
    }
    
    // Public method to update boundaries if needed
    public void RefreshBoundaries()
    {
        UpdateBoundaries();
    }
}
