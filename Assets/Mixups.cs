using UnityEngine;

public class mixups : MonoBehaviour
{
    public GameObject rat;
    private int difficulty;
    private P1 player;
    private BallManager ballManager;
    void Start()
    {
        player = FindFirstObjectByType<P1>();
    }

    public void DoMixup()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                SpawnBall();
                break;
            case 1:
                SpawnRat();
                break;
            case 2:
                RotateScreen();
                break;
            case 3:
                SuperPaddle();
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

    void RotateScreen()
    {
        RotateMixUp.Instance.DoMixUp();
    }

    void SpawnRat()
    {
        Instantiate(rat, randinbounds(),transform.rotation);
    }

    Vector2 randinbounds()
    {
        return new Vector2(Random.Range(-8.5f, 8.5f),Random.Range(-4.5f,4.5f));
    }

    void SpawnGorilla()
    {

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
        if (GUILayout.Button("Rotate Screen"))
        {
            RotateScreen();
        }
    }
#endif
}
