using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Calculate direction towards the center of the screen
        Vector3 screenCenterWorld = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector2 direction = (screenCenterWorld - transform.position).normalized;
        
        rb.velocity = direction * speed;
    }
}
