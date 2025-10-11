using System.Numerics;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rb;
    public float startingsSpeed;
    public bool freezeRotation; //if true, freeze rotation of ball
    public UnityEngine.Vector2 startLocation;

    public SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (freezeRotation == true)
        {
            rb.freezeRotation = true;
        }

        if (startLocation != null)
        {
            transform.position = startLocation;
        }
        
        bool isRight = UnityEngine.Random.value >= 0.5f;

        float xVelocity = -1f;

        if (isRight == true)
        {
            xVelocity = 1f;
        }

        float yVelocity = UnityEngine.Random.Range(-1f, 1f);

        rb.linearVelocity = new UnityEngine.Vector2(xVelocity * startingsSpeed, yVelocity * startingsSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
