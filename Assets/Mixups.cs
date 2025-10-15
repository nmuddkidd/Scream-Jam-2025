using UnityEngine;

public class mixups : MonoBehaviour
{
    private int difficulty;
    private P1 player;
    private BallManager ballManager;
    void Start()
    {
        player = FindFirstObjectByType<P1>();
    }

    public void DoMixup()
    {
        SpawnBall();
        switch (Random.Range(0, 10))
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
        }
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

    void SpawnObstacle()
    {

    }

    void AltF4Spoof()
    {

    }

    void ControlChange()
    {

    }

    void SuperPaddle()
    {        
        SuperPaddleMixup.Instance.DoMixUp();
    }

// For testing mixups in editor
#if UNITY_EDITOR
    void OnGUI()
    {
        if (GUILayout.Button("Do Mixup"))
        {
            DoMixup();
        }
        if (GUILayout.Button("Super Paddle"))
        {
            SuperPaddle();
        }
    }
#endif
}
