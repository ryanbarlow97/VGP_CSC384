using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    public GameObject[] smallMeteorPrefabs;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb.velocity == Vector2.zero)
        {
            // Calculate direction towards the center of the screen
            Vector3 screenCenterWorld = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
            Vector2 direction = (screenCenterWorld - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }
    public void SpawnSmallerMeteors()
    {
        if (smallMeteorPrefabs != null && smallMeteorPrefabs.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject smallMeteor = Instantiate(smallMeteorPrefabs[i], transform.position, Quaternion.identity);
                MeteorMovement smallMeteorMovement = smallMeteor.GetComponent<MeteorMovement>();

                // Generate a random angle in degrees
                float angle = Random.Range(0f, 360f);

                // Convert the angle to a direction vector
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                smallMeteorMovement.SetDirection(direction);
            }
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }
}
