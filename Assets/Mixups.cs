using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using TMPro;
using System.Threading;

public class mixups : MonoBehaviour
{
    //put in new event audio
    public AudioClip neweventsnd;
    private AudioSource neweventAudio;
    public AudioClip Gorillasnd;
    private AudioSource GorillaAudio;
    public GameObject rat;
    public GameObject gorilla;
    public TMP_Text mixuptext;
    private P1 player;
    private BallManager ballManager;
    private float Timer;
    void Start()
    {
        //start new event audio
        neweventAudio = GetComponent<AudioSource>();

        player = FindFirstObjectByType<P1>();
        
        //start gorilla audio
        GorillaAudio = GetComponent<AudioSource>();
    }

    public void DoMixup()
    {
        //play new event audio
        neweventAudio.PlayOneShot(neweventsnd, 2.0f);

        switch (UnityEngine.Random.Range(0, 10))
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
                ControlChange();
                break;
            case 6:
                SpawnGorilla();
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
            UnityEngine.Debug.LogError("ControlChangeMixup.Instance is null � make sure the object is in the scene and initialized.");
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
        Instantiate(rat, randinbounds(), transform.rotation);
    }

    Vector2 randinbounds()
    {
        return new Vector2(UnityEngine.Random.Range(-8.5f, 8.5f), UnityEngine.Random.Range(-4.5f,4.5f));
    }

    void SpawnGorilla()
    {
        mixuptext.text = "goilla";
        
        Instantiate(gorilla, randinbounds(), transform.rotation);
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
        if (GUILayout.Button("Control Change"))
        {
            ControlChange();
        }
        if (GUILayout.Button("Spawn Gorilla"))
        {
            SpawnGorilla();
        }
    }
#endif
}
/*if (GUILayout.Button("Speed Up/Slow Down"))
{
    SpeedUpSlowDown();
}
*/