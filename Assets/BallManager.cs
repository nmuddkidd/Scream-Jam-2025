using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;
    public AudioClip aiscoresnd;
    public AudioClip playerscoresnd;
    private AudioSource scoreAudio;

    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Vector2 spawnPosition = Vector2.zero;
    public float initialSpeed = 8f;

    [Header("Spawn Settings")]
    public float respawnDelay = 3f;
    [HideInInspector]
    public List<GameObject> activeBalls = new List<GameObject>();

    public UnityEvent<Ball> OnBallSpawnedEvent;
    private StateController stateController;
    private bool isRespawnScheduled = false;

    public System.Action<GameObject> OnBallSpawned;
    public System.Action<GameObject> OnBallDestroyed;
    public System.Action OnAllBallsDestroyed;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        scoreAudio = GetComponent<AudioSource>();

        Debug.Log("BallManager: Starting up...");
        stateController = FindFirstObjectByType<StateController>();
        if (stateController == null)
        {
            Debug.LogError("BallManager: StateController not found!");
        }
        else
        {
            Debug.Log("BallManager: StateController found successfully!");
        }
    }

    void Update()
    {
        // Keep trying to find StateController if we don't have it
        if (stateController == null)
        {
            stateController = FindFirstObjectByType<StateController>();
        }
        
        // Clean up destroyed balls
        CleanupDestroyedBalls();
    }
    
    public GameObject SpawnBall()
    {
        Debug.Log("BallManager: SpawnBall() called");
        return SpawnBall(initialSpeed);
    }
    
    public GameObject SpawnBall(float customBaseSpeed)
    {
        Debug.Log($"BallManager: SpawnBall({customBaseSpeed}) called");
        if (ballPrefab == null)
        {
            Debug.LogError("BallManager: Ball prefab is not assigned!");
            return null;
        }
        
        GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Ball ballScript = newBall.GetComponent<Ball>();
        
        if (ballScript != null)
        {
            // Get random direction (left or right)
            bool isRight = Random.value >= 0.5f;
            float xDirection = isRight ? 1f : -1f;
            float yDirection = Random.Range(-0.5f, 0.5f);
            Vector2 direction = new Vector2(xDirection, yDirection);

            ballScript.Initialize(customBaseSpeed, direction);
            
            OnBallSpawnedEvent?.Invoke(ballScript);
        }
        
        activeBalls.Add(newBall);
        OnBallSpawned?.Invoke(newBall);
        
        Debug.Log($"Ball spawned. Active balls: {activeBalls.Count}");
        return newBall;
    }
    
    public void OnBallScored(GameObject ball, bool aiScored)
    {
        // Remove and destroy the scoring ball
        DestroyBall(ball);
        
        // Notify StateController
        if (stateController != null)
        {
            if (aiScored)
            {
                scoreAudio.PlayOneShot(aiscoresnd, 1.0f);
                stateController.AIScored();
            }
            else
            {
                scoreAudio.PlayOneShot(playerscoresnd, 1.0f);
                stateController.PlayerScored(stateController.goalScored, transform.position);
            }
        }
        
        // Check if we need to spawn a new ball
        CheckForRespawn();
    }
    
    public void DestroyBall(GameObject ball)
    {
        if (ball != null && activeBalls.Contains(ball))
        {
            activeBalls.Remove(ball);
            OnBallDestroyed?.Invoke(ball);
            Destroy(ball);
            
            Debug.Log($"Ball destroyed. Active balls: {activeBalls.Count}");
            
            if (activeBalls.Count == 0)
            {
                OnAllBallsDestroyed?.Invoke();
            }
        }
    }
    
    public void DestroyAllBalls()
    {
        for (int i = activeBalls.Count - 1; i >= 0; i--)
        {
            if (activeBalls[i] != null)
            {
                Destroy(activeBalls[i]);
            }
        }
        
        activeBalls.Clear();
        OnAllBallsDestroyed?.Invoke();
        Debug.Log("All balls destroyed");
    }
    
    private void CheckForRespawn()
    {
        // Only spawn new ball if list is empty and player still has lives
        if (activeBalls.Count == 0 && !isRespawnScheduled)
        {
            if (stateController != null && stateController.playerLives > 0)
            {
                StartCoroutine(RespawnAfterDelay());
            }
        }
    }
    
    private IEnumerator RespawnAfterDelay()
    {
        isRespawnScheduled = true;
        Debug.Log($"Respawning ball in {respawnDelay} seconds...");
        
        yield return new WaitForSeconds(respawnDelay);
        
        // Double-check that we still need a ball and player has lives
        if (activeBalls.Count == 0 && stateController != null && stateController.playerLives > 0)
        {
            SpawnBall();
        }
        
        isRespawnScheduled = false;
    }
    
    private void CleanupDestroyedBalls()
    {
        activeBalls.RemoveAll(ball => ball == null);
    }
    
    public int GetActiveBallCount()
    {
        CleanupDestroyedBalls();
        return activeBalls.Count;
    }
    
    public bool HasActiveBalls()
    {
        CleanupDestroyedBalls();
        return activeBalls.Count > 0;
    }

    public GameObject GetFirstBall()
    {
        CleanupDestroyedBalls();
        return activeBalls.Count > 0 ? activeBalls[0] : null;
    }

    public GameObject GetBestBall(float xorigin)
    {
        CleanupDestroyedBalls();
        if (activeBalls.Count > 1)
        {
            int index = 0;
            for (int i = 1; i < activeBalls.Count; i++)
            {
                if (activeBalls[i].GetComponent<Rigidbody2D>().linearVelocity.x < 0 &&
                    activeBalls[i].transform.position.x - xorigin < activeBalls[index].transform.position.x - xorigin)
                {
                    index = i;
                }
            }
            return activeBalls[index];
        }
        return activeBalls[0];
    }
}
