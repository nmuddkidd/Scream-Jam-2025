using UnityEngine;
using UnityEngine.InputSystem;

public class P1 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    // input key member for mixup to change
    public static Key keyboardUpKey = Key.W;
    public static Key keyboardDownKey = Key.S;

    [Header("Boundary Objects")]
    public GameObject topBoundary;
    public GameObject bottomBoundary;


    // Boundary limits
    private float topLimit;
    private float bottomLimit;
    private float paddleHalfHeight;

    // Game state management
    private StateController stateController;
    private bool isEnabled = true;

    void Start()
    {
        // Find StateController
        stateController = FindFirstObjectByType<StateController>();

        // Set up boundaries
        UpdateBoundaries();
    }

    void UpdateBoundaries()
    {
        // Get paddle half-height for collision detection
        Renderer paddleRenderer = GetComponent<Renderer>();
        if (paddleRenderer != null)
        {
            paddleHalfHeight = paddleRenderer.bounds.size.y / 2f;
        }

        // Get boundary positions
        if (topBoundary != null)
        {
            Renderer topRenderer = topBoundary.GetComponent<Renderer>();
            topLimit = topRenderer.bounds.min.y - paddleHalfHeight;
        }
        else
        {
            topLimit = 5f; // Default fallback
        }

        if (bottomBoundary != null)
        {
            Renderer bottomRenderer = bottomBoundary.GetComponent<Renderer>();
            bottomLimit = bottomRenderer.bounds.max.y + paddleHalfHeight;
        }
        else
        {
            bottomLimit = -5f; // Default fallback
        }

        Debug.Log($"P1: Boundaries set - Top: {topLimit}, Bottom: {bottomLimit}");
    }

    void Update()
    {
        // Only move if enabled and game allows player movement
        if (!isEnabled && stateController == null! && stateController.CanPlayerMove()){
            return;
        }

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition;

        // Handle input
        if (Keyboard.current[keyboardUpKey].isPressed)
        {
            newPosition.y += moveSpeed * Time.deltaTime;
        }
        if (Keyboard.current[keyboardDownKey].isPressed)
        {
            newPosition.y -= moveSpeed * Time.deltaTime;
        }

        // Clamp position within boundaries
        newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit, topLimit);

        transform.position = newPosition;
    }

    // Called by StateController
    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
        Debug.Log($"P1: Paddle {(enabled ? "enabled" : "disabled")}");
    }

    // Public method to update boundaries if needed
    public void RefreshBoundaries()
    {
        UpdateBoundaries();
    }
}
