using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] smallMeteorPrefabs;

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

            // Get the bullet impact direction
            Vector2 bulletImpactDirection = collision.GetContact(0).normal * -1;

            // Spawn smaller meteors
            ICommand spawnSmallerMeteorsCommand = new SpawnSmallerMeteorsCommand(meteorMovement.smallMeteorPrefabs, transform.position, meteorMovement.speed / 2, -collision.contacts[0].normal);

            
            spawnSmallerMeteorsCommand.Execute();

            // Destroy the meteor
            Destroy(gameObject);

            //Destroy explosion after 1.25 seconds
            Destroy(newExplosion, 1.25f);
        }
    }

}
