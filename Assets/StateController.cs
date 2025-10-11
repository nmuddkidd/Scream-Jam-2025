using System.Collections;
using System.Diagnostics;
using UnityEngine;

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

    private float mixUpTimer;
    private bool waitingForInput; //waiting for player to press a key to start/restart

    public enum GameState
    {
        Start,
        Playing,
        GameEnd,
        Results
    }
    //Mixup Call
    public Mixups mixups;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGame();
        GameObject targetGameObject = GameObject.Find("Mixups"); // Find by name
                                                                           // Or: GameObject targetGameObject = GameObject.FindGameObjectWithTag("TargetTag"); // Find by tag

    // Update is called once per frame
    void Update()
    {
        HandleCurrentState();
        HandleInput();
    }

    void InitializeGame()
    {
        currentState = GameState.Start;
        playerLives = startingLives;
        playerScore = 0;
        currentScoreThreshold = baseScoreThreshold;
        mixUpTimer = mixUpInterval;
        waitingForInput = true;
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

    public void PlayerScored(int points)
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
    }

    public void AIScored()
    {
        playerLives--;
        Debug.Log("Life lost | Lives: " + playerLives);

        if (playerLives <= 0)
        {
            EndGame();
        }
        else
        {
            SpawnBall();
        }
    }

    void EndGame()
    {
        currentState = GameState.GameEnd;
        waitingForInput = false;
        Debug.Log("Game Over");

        DisablePaddles();
    }

    void GoToResults()
    {
        currentState = GameState.Results;
        Debug.Log($"Final Score: {playerScore} | Press any key to restart");
    }

    void TriggerMixUp()
    {
            if (targetGameObject != null)
            {
                targetScript = targetGameObject.GetComponent<TargetScript>();
                if (targetScript != null)
                {
                    targetScript.start();
                }
                else
                {
                    Debug.LogError("TargetScript not found on TargetGameObject!");
                }
            }
            else
            {
                Debug.LogError("TargetGameObject not found!");
            }
        }
    }

    void SpawnBall()
    {
        //activate ball
    }

    void EnablePaddles()
    {
        //enable player and AI paddles
    }

    void DisablePaddles()
    {
        //disable player and AI paddles
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
