using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the meteor
    private Rigidbody2D rb; // Rigidbody of the meteor

    public GameObject[] smallMeteorPrefabs; // Array of small meteor prefabs
    private ICommand moveCommand; // Command to move the meteor
    private Vector2 direction; // Direction to move the meteor

    public bool useInitialDirection = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!useInitialDirection)
        {
            // Calculate direction towards the center of the screen
            Vector3 screenCenterWorld = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)); // Get the center of the screen in world coordinates
            direction = (screenCenterWorld - transform.position).normalized; // Normalize the direction
        }

        moveCommand = new MoveCommand(rb, speed, Mathf.Infinity, direction); // Create the move command
    }

    void Update()
    {
        moveCommand.Execute(); 
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        moveCommand = new MoveCommand(rb, speed, Mathf.Infinity, newDirection);
    }
}
