using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] smallMeteorPrefabs;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);

            // Spawn an explosion at the meteor's position
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Get the bullet impact direction
            Vector2 bulletImpactDirection = collision.GetContact(0).normal * -1;

            // Get the meteor's speed at the time of collision
            float meteorSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;

            // Spawn smaller meteors
            ICommand spawnSmallerMeteorsCommand = new SpawnSmallerMeteorsCommand(
                smallMeteorPrefabs, transform.position, meteorSpeed / 2, -collision.contacts[0].normal, transform.localScale.x);
            spawnSmallerMeteorsCommand.Execute();

            // Destroy the meteor
            Destroy(gameObject);

            // Destroy explosion after 1.25 seconds
            Destroy(newExplosion, 0.8f);
        }

        if (collision.collider.CompareTag("PlayerShip"))
        {
            Debug.Log("PlayerLost");
        }
    }
}
