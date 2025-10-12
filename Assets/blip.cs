using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class blip : MonoBehaviour
{
    //Physics
    public Rigidbody2D Rigidbody;
    public int upspeed;

    //Death timing
    public int timeout;
    private float timer;

    //Display
    public int points;
    public TMP_Text text;
    void Start()
    {
        Rigidbody.linearVelocity = Vector2.up * upspeed;
        text.text = points.ToString();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeout)
        {
            Destroy(gameObject);
        }
    }
}
