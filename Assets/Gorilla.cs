using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class Gorilla : MonoBehaviour
{

    public GameObject gorilla;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float jumpForce = 1200f;
    public float xSpeed = 5f;
    public float jumpInterval = 2f;
    public float nextJumpTime;
    public float direction = 1f;
    public float leftBound = -7.5f;
    public float rightBound = 7.5f;
    public static Gorilla Instance;



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
        if (gorilla.transform.position.x <= leftBound + buffer && direction < 0f)
        {
            direction = 1f;
        }
        else if (gorilla.transform.position.x >= rightBound - buffer && direction > 0f)
        {
            direction = -1f;
        }
        if (Time.time >= nextJumpTime && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            nextJumpTime = Time.time + jumpInterval;
        }

    }
    IEnumerator SpawnGorillaRoutine()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(0, -4.2F);
            GameObject spawnedGorilla = Instantiate(gorilla, spawnPosition, transform.rotation);
            float waitTime = Random.Range(10f, 20f);
            Debug.Log("Gorilla Spawned");
            // Wait for a random time between 10 and 20 seconds before spawning the next gorilla
            yield return new WaitForSeconds(waitTime);
            Destroy(spawnedGorilla, 5f);
        }
    }
    public void DoMixUp()
    {
        StartCoroutine(SpawnGorillaRoutine());
    }
    
}
