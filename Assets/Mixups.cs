using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class mixups : MonoBehaviour
{
    public GameObject rat;
    public GameObject gorilla;
    private int difficulty;
    private P1 player;
    private BallManager ballManager;
    void Start()
    {
        player = FindFirstObjectByType<P1>();
    }

    public void DoMixup()
    {
        switch (UnityEngine.Random.Range(0, 6))
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
                //SpeedUpSlowDown();
                break;
            case 5:
<<<<<<< Updated upstream
                ControlChange();
=======
                SpawnGorilla();
>>>>>>> Stashed changes
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
            UnityEngine.Debug.Log("StateController: Calling BallManager.SpawnBall()");
            ballManager.SpawnBall();
        }
        else
        {
            UnityEngine.Debug.LogError("StateController: Cannot spawn ball - BallManager still not found! Retrying in 0.5 seconds...");
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

        if (ControlChangeMixup.Instance != null)
        {
            StartCoroutine(ControlChangeMixup.Instance.DoMixUp());
        }
        else
        {
            UnityEngine.Debug.LogError("ControlChangeMixup.Instance is null — make sure the object is in the scene and initialized.");
        }

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
        return new Vector2(UnityEngine.Random.Range(-8.5f, 8.5f), UnityEngine.Random.Range(-4.5f,4.5f));
    }

    void SpawnGorilla()
    {
        Gorilla.Instance.DoMixUp();
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
<<<<<<< Updated upstream
        if (GUILayout.Button("Speed Up/Slow Down"))
        {
            SpeedUpSlowDown();
        }
        if (GUILayout.Button("Control Change"))
        {
            ControlChange();
        }
=======
>>>>>>> Stashed changes
    }
#endif
}