using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class Gorilla : MonoBehaviour
{

    private Rigidbody2D rb;
    public float jumpForce;
    public float xSpeed;
    public float jumpInterval;
    private float nextJumpTime;
    private float direction;
    public float leftBound;
    public float rightBound;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        nextJumpTime = Time.time + jumpInterval;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float buffer = 0.05f;
        rb.linearVelocity = new Vector2(direction * xSpeed, rb.linearVelocity.y);
        if (transform.position.x <= leftBound + buffer && direction < 0f)
        {
            direction = 1f;
        }
        else if (transform.position.x >= rightBound - buffer && direction > 0f)
        {
            direction = -1f;
        }
        if (Time.time >= nextJumpTime && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            nextJumpTime = Time.time + jumpInterval;
        }

    }
}
