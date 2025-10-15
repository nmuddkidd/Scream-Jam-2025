using UnityEngine;

public class SuperPaddleRegion : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }
        print("Collision Entered");
        var ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            OnRegionHit(ball);
        }
    }

    void OnRegionHit(Ball ball)
    {
        ball.ModifySpeed(1.5f);
        //sfx call here
    }
}
