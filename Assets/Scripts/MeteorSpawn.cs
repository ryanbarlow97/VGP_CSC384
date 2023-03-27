using System.Collections;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRate = 1f;
    private Camera mainCamera;
    private float screenOffset = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            Vector3 spawnPosition = Vector3.zero;
            Quaternion spawnRotation = Quaternion.identity;

            // Choose a random edge: 0 = Top, 1 = Bottom, 2 = Left, 3 = Right
            int randomEdge = Random.Range(0, 4);

            // Recalculate screen boundaries each time a meteor is spawned
            float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x + screenOffset;
            float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x - screenOffset;
            float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y + screenOffset;
            float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)).y - screenOffset;

            // Randomly select the direction and speed of the meteor
            Vector2 meteorDirection = Vector2.zero;
            float meteorSpeed = 0f;
            switch (randomEdge)
            {
                case 0: // Top
                    meteorDirection = Vector2.down;
                    meteorSpeed = Random.Range(5f, 10f);
                    spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenTop, 0f);
                    break;
                case 1: // Bottom
                    meteorDirection = Vector2.up;
                    meteorSpeed = Random.Range(5f, 10f);
                    spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenBottom, 0f);
                    break;
                case 2: // Left
                    meteorDirection = Vector2.right;
                    meteorSpeed = Random.Range(5f, 10f);
                    spawnPosition = new Vector3(screenLeft, Random.Range(screenBottom, screenTop), 0f);
                    break;
                case 3: // Right
                    meteorDirection = Vector2.left;
                    meteorSpeed = Random.Range(5f, 10f);
                    spawnPosition = new Vector3(screenRight, Random.Range(screenBottom, screenTop), 0f);
                    break;
            }

            // Calculate the meteor's initial rotation
            spawnRotation = Quaternion.LookRotation(Vector3.forward, meteorDirection);

            // Scale the meteor size based on a random range between 1 and 3
            float scale = Random.Range(0.5f, 2f);
            meteorPrefab.transform.localScale = new Vector3(scale, scale, 1);

            // Set the meteor speed based on the size
            meteorSpeed = Random.Range(5f, 10f) * scale;


            // Instantiate the meteor and set its initial velocity
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, spawnRotation);
            Rigidbody2D meteorRigidbody = meteor.GetComponent<Rigidbody2D>();
            meteorRigidbody.velocity = meteorDirection * meteorSpeed;

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
