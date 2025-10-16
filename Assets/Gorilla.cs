using UnityEngine;

public class Gorilla : MonoBehaviour
{


    private Rigidbody2D rb;
    public GameObject PointA;
    public GameObject PointB;
    private Transform currentPoint;
    public float jumpForce = 8f;
    public float xSpeed = 5f;
    public float jumpInterval = 2f;
    public float nextJumpTime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointA.transform;
        nextJumpTime = Time.time + jumpInterval;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
       
            if (currentPoint == PointA.transform)
            {
                rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-xSpeed, rb.linearVelocity.y);
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == PointA.transform)
            {
                currentPoint = PointB.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == PointB.transform)
            {
                currentPoint = PointA.transform;
            }
            if (Time.time >= nextJumpTime && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpForce), ForceMode2D.Impulse);
            nextJumpTime = Time.time + jumpInterval;
            }
            
        
    }
}
