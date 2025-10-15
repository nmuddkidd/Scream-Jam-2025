using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    //settings
    public int startingLives = 3;
    public int baseScoreThreshold = 100;
    public float mixUpInterval = 30f; //sec

    //state
    public GameState currentState = GameState.Start;
    public int playerLives;
    public int playerScore;
    public int currentScoreThreshold;

    //game references
    private BallManager ballManager;
    private Transform ballSpawnPoint;
    private P1 playerPaddle;
    private A1 aiPaddle;

    private float mixUpTimer;
    private bool waitingForInput; //waiting for player to press a key to start/restart
    private mixups mixups;

    //UI
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public GameObject blip;
    public enum GameState
    {
        Start,
        Playing,
        GameEnd,
        Results
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGame();
        mixups = FindFirstObjectByType<mixups>();
        // Find BallManager reference
        ballManager = FindFirstObjectByType<BallManager>();

        if (ballManager == null)
        {
            Debug.LogError("StateController: BallManager not found! Make sure BallManager script is attached to a GameObject in the scene.");
            
            // Try alternative search method
            GameObject ballManagerObj = GameObject.Find("BallManager");
            if (ballManagerObj != null)
            {
                ballManager = ballManagerObj.GetComponent<BallManager>();
                if (ballManager != null)
                {
                    Debug.Log("StateController: Found BallManager using GameObject.Find method.");
                }
                else
                {
                    Debug.LogError("StateController: BallManager GameObject found but no BallManager script attached!");
                }
            }
        }
        else
        {
            Debug.Log("StateController: BallManager found successfully!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleCurrentState();
        HandleInput();
    }

    void InitializeGame()
    {
        // TEMP: Skip start state and go directly to playing for testing
        currentState = GameState.Playing; // Changed from GameState.Start
        playerLives = startingLives;
        playerScore = 0;
        currentScoreThreshold = baseScoreThreshold;
        mixUpTimer = mixUpInterval;
        waitingForInput = false; // Changed from true
        
        // Delay auto-start to ensure all components are ready
        Invoke(nameof(DelayedGameStart), 0.1f);
        Debug.Log("Game initialization complete, starting in 0.1 seconds...");
    }
    
    void DelayedGameStart()
    {
        // Auto-start the game after brief delay
        SpawnBall();
        EnablePaddles();
        Debug.Log("Game Auto-Started (bypassing start state)");
    }

    void HandleCurrentState()
    {
        switch (currentState)
        {
            case GameState.Start:
                //wait for user input
                break;

            case GameState.Playing:
                mixUpTimer -= Time.deltaTime;
                if (mixUpTimer <= 0)
                {
                    TriggerMixUp();
                    mixUpTimer = mixUpInterval;
                }
                break;

            case GameState.GameEnd:
                if (waitingForInput)
                {
                    Invoke(nameof(GoToResults), 2f);
                    waitingForInput = true;
                }
                break;

            case GameState.Results:
                //wait for user input
                break;
        }
    }

    void HandleInput()
    {
        // TEMP: Input handling disabled while using new Input System
        // TODO: Implement proper Input System handling later
        
        /* 
        switch (currentState)
        {
            case GameState.Start:
                if (Input.anyKeyDown)
                {
                    StartGame();
                }
                break;

            case GameState.Results:
                if (Input.anyKeyDown)
                {
                    RestartGame();
                }
                break;
        }
        */
    }

    void StartGame()
    {
        currentState = GameState.Playing;
        Debug.Log("Game Started");

        SpawnBall();
        EnablePaddles();
    }

    void RestartGame()
    {
        InitializeGame();
        Debug.Log("Game Restarted");
    }

    public void PlayerScored(int points, Vector2 coordinates)
    {
        if (points != 0)
        {
            playerScore += points;
        }
        else
        {
            playerScore++;
        }
        
        Debug.Log("Score: " + playerScore);

        if (playerScore >= currentScoreThreshold)
        {
            playerLives++;
            currentScoreThreshold = playerScore * 2; //threshold to get life doubles
            Debug.Log("Extra life added | Lives: " + playerLives);
        }

        scoreText.text = playerScore.ToString();
        GameObject preblip = Instantiate(blip);
        preblip.transform.position = coordinates;
        blip script = preblip.GetComponent<blip>();
        script.points = points;
    }

    public void AIScored()
    {
        playerLives--;
        Debug.Log("Life lost | Lives: " + playerLives);

        if (playerLives <= 0)
        {
            EndGame();
        }
        // Note: BallManager handles respawning automatically after delay
        // Only if playerLives > 0
        String healthbar = "";
        for (int i = 0; i < playerLives; i++)
        {
            healthbar += '|';
        }
        healthText.text = healthbar;
    }

    void EndGame()
    {
        currentState = GameState.GameEnd;
        waitingForInput = false;
        Debug.Log("Game Over");

        DisablePaddles();
        
        // Destroy all balls when game ends
        if (ballManager != null)
        {
            ballManager.DestroyAllBalls();
        }
    }

    void GoToResults()
    {
        currentState = GameState.Results;
        Debug.Log($"Final Score: {playerScore} | Press any key to restart");
    }

    void TriggerMixUp()
    {
        mixups = FindFirstObjectByType<mixups>();
        mixups.DoMixup();
    }

    void SpawnBall()
    {
        // Try to find BallManager again if we don't have it
        if (ballManager == null)
        {
            ballManager = FindFirstObjectByType<BallManager>();
        }
        
        if (ballManager != null)
        {
            Debug.Log("StateController: Calling BallManager.SpawnBall()");
            ballManager.SpawnBall();
        }
        else
        {
            Debug.LogError("StateController: Cannot spawn ball - BallManager still not found! Retrying in 0.5 seconds...");
            // Retry after a short delay
            Invoke(nameof(SpawnBall), 0.5f);
        }
    }

    void EnablePaddles()
    {
        Debug.Log("StateController: Enabling paddles");
        
        // Find and enable player paddle
        if (playerPaddle == null)
        {
            playerPaddle = FindFirstObjectByType<P1>();
        }
        if (playerPaddle != null)
        {
            playerPaddle.SetEnabled(true);
        }
        else
        {
            Debug.LogWarning("StateController: P1 paddle not found!");
        }
        
        // Find and enable AI paddle
        if (aiPaddle == null)
        {
            Debug.Log("StateController: Looking for A1 paddle...");
            aiPaddle = FindFirstObjectByType<A1>();
            if (aiPaddle != null)
            {
                Debug.Log("StateController: A1 paddle found!");
            }
            else
            {
                Debug.Log("StateController: A1 paddle NOT found by FindFirstObjectByType!");
            }
        }
        if (aiPaddle != null)
        {
            Debug.Log("StateController: Enabling A1 paddle");
            aiPaddle.SetEnabled(true);
        }
        else
        {
            Debug.LogWarning("StateController: A1 paddle not found!");
        }
    }

    void DisablePaddles()
    {
        Debug.Log("StateController: Disabling paddles");
        
        if (playerPaddle != null)
        {
            playerPaddle.SetEnabled(false);
        }
        if (aiPaddle != null)
        {
            aiPaddle.SetEnabled(false);
        }
    }

    public bool IsGamePlaying()
    {
        return currentState == GameState.Playing;
    }

    public bool CanPlayerMove()
    {
        return currentState == GameState.Playing;
    }
}
