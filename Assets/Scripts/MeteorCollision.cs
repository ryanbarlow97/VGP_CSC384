using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    public GameObject explosionPrefab;

    private MeteorMovement meteorMovement;

    private void Start()
    {
        meteorMovement = GetComponent<MeteorMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);

            // Spawn an explosion at the meteor's position
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Spawn smaller meteors
            meteorMovement.SpawnSmallerMeteors();

            // Destroy the meteor
            Destroy(gameObject);
            //Destroy explosion after 1.25 seconds
            Destroy(newExplosion, 1.25f);
        }
    }
}
