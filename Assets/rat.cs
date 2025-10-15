using UnityEngine;

public class rat : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private Vector2 desiredpos;
    void Start()
    {
        newdesired();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,desiredpos,speed*Time.deltaTime);
        if (new Vector2(transform.position.x, transform.position.y) == desiredpos)
        {
            newdesired();
        }
        Debug.Log(transform.position+" "+desiredpos);
    }

    void newdesired()
    {
        desiredpos = new Vector2(Random.Range(-8.5f, 8.5f),Random.Range(-4.5f,4.5f));
    }
}
