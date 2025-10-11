using UnityEngine;
using UnityEngine.InputSystem;

public class P1 : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
    }
}
