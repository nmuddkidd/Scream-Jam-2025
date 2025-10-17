using UnityEngine;

public class GravityBallsMixUp : MonoBehaviour
{
    public static GravityBallsMixUp Instance;
    public float duration = 10f;
    float starttime;

    void Awake()
    {
        Instance = this;
        starttime = float.MaxValue;
    }

    void Update()
    {
        if (Time.unscaledTime - starttime >= duration)
        {
            CleanUp();
        }
    }

    public void DoMixUp()
    {
        starttime = Time.unscaledTime;
        foreach (var ball in BallManager.Instance.activeBalls)
        {
            ball.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        BallManager.Instance.OnBallSpawnedEvent.AddListener(addGravitytoBall);
    }

    void CleanUp()
    {
        BallManager.Instance.OnBallSpawnedEvent.RemoveListener(addGravitytoBall);
        foreach (var ball in BallManager.Instance.activeBalls)
        {
            ball.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    void addGravitytoBall(Ball ball)
    {
        ball.GetComponent<Rigidbody2D>().gravityScale = 1f;
    }
}
