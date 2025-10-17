using UnityEngine;

public class rat : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private Vector2 desiredpos;

    [SerializeField]
    float minduration = 10f, maxduration = 30f;
    float duration;
    float starttime;
    void Start()
    {
        newdesired();
        duration = Random.Range(minduration, maxduration);
        starttime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,desiredpos,speed*Time.deltaTime);
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), desiredpos) < 0.01f)
        {
            newdesired();
        }
        //Debug.Log(transform.position + " " + desiredpos);
        if (Time.realtimeSinceStartup - starttime >= duration)
        {            
            duration = Random.Range(minduration, maxduration);
            starttime = Time.realtimeSinceStartup;
            Destroy(gameObject);
        }
    }

    void newdesired()
    {
        desiredpos = new Vector2(Random.Range(-8.5f, 8.5f),Random.Range(-4.5f,4.5f));
    }
}
