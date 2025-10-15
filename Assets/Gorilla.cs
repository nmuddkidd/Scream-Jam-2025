using UnityEngine;

public class Gorilla : MonoBehaviour
{
    public GameObject Gorilla_;
    public GameObject Ball;
    public Rigidbody2D rb;
    private float Xcoord;
    private float Ycoord;

    public float speed = 5f;

    public void SetTarget(GameObject Ball)
    {
        rb.linearVelocity = Vector2.MoveTowards(Ball.transform.position, Gorilla_.transform.position, speed);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
