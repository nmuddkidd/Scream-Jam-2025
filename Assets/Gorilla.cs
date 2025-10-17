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
    public float leftBound;
    public float rightBound;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextJumpTime = Time.time + jumpInterval;
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
    }

    void Update()
    {
        float buffer = 0.05f;
        //rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
        if (transform.position.x <= leftBound + buffer && rb.linearVelocity.x < 0f)
        {
            xSpeed *= -1;
        }
        else if (transform.position.x >= rightBound - buffer && rb.linearVelocity.x > 0f)
        {
            xSpeed *= -1;
        }
        if (Time.time >= nextJumpTime && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce * Random.Range(.6f,1f), ForceMode2D.Impulse);
            nextJumpTime = Time.time + jumpInterval * Random.Range(.5f,2f);
        }

    }
}
