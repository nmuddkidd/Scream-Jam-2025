using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip playerinteractsnd;
    public AudioClip ballscollidesnd;
    private AudioSource playerAudio;

    private AudioSource ballcollideAudio;

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
    public GameObject particle;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        ballcollideAudio = GetComponent<AudioSource>();
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

        rb.linearVelocity += new Vector2(UnityEngine.Random.Range(-0.01f, 0.01f), UnityEngine.Random.Range(-0.01f, 0.01f));
        if(rb.linearVelocity.magnitude<baseSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * baseSpeed;
        }
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("AP1") || collision.gameObject.name.Equals("UP1"))
        {
            //Although this is same sound I don't have pitch randomizer for Racket sounds
            playerAudio.PlayOneShot(playerinteractsnd);  
            stateController.PlayerScored(10, gameObject.transform.position);          

            // Not sure what this was meant to do, commenting out for now. We can reply on unity physics for bounce.

            // float theta = Vector2.SignedAngle(collision.gameObject.transform.position, gameObject.transform.position);
            // rb.linearVelocity = new Vector2(rb.linearVelocity.magnitude * math.cos(theta),rb.linearVelocity.magnitude * math.sin(theta)) *-1;
        }
        else if (collision.gameObject.name.Equals("BallPrefab(Clone)"))
        {
            //Simply to have a diff sound effect for when Balls collide (it does play twice but I like it)
            if (!ballcollideAudio.isPlaying)
            ballcollideAudio.PlayOneShot(ballscollidesnd);
            Instantiate(particle, transform.position, Quaternion.identity);
            stateController.PlayerScored(1, gameObject.transform.position);
        }
        else
        {
            //Will play same sound with randomized pitch for every other ball collision instance cuz it feels less repetitive
            playerAudio.pitch = UnityEngine.Random.Range(1f, 2f);
            playerAudio.PlayOneShot(playerinteractsnd);
            stateController.PlayerScored(1, gameObject.transform.position);
        }        
    }
}
