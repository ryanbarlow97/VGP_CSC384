using System.Collections;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRate = 1f;
    private Camera mainCamera;
    private float screenOffset = 2f;

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
            Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            float minX = screenBottomLeft.x + screenOffset;
            float maxX = screenTopRight.x - screenOffset;
            float minY = screenBottomLeft.y + screenOffset;
            float maxY = screenTopRight.y - screenOffset;

            // Randomly select the direction and speed of the meteor
            Vector2 meteorDirection = Vector2.zero;
            float meteorSpeed = 0f;
            switch (randomEdge)
            {
                case 0: // Top
                    meteorDirection = Vector2.down;
                    meteorSpeed = Random.Range(5f, 10f);
                    break;
                case 1: // Bottom
                    meteorDirection = Vector2.up;
                    meteorSpeed = Random.Range(5f, 10f);
                    break;
                case 2: // Left
                    meteorDirection = Vector2.right;
                    meteorSpeed = Random.Range(5f, 10f);
                    break;
                case 3: // Right
                    meteorDirection = Vector2.left;
                    meteorSpeed = Random.Range(5f, 10f);
                    break;
            }

            // Calculate the meteor's initial position and rotation
            spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), mainCamera.nearClipPlane));
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, minX, maxX);
            spawnPosition.y = Mathf.Clamp(spawnPosition.y, minY, maxY);
            spawnPosition.z = 0f;
            spawnRotation = Quaternion.LookRotation(Vector3.forward, meteorDirection);

            // Scale the meteor size based on its speed
            float scale = Mathf.Clamp(meteorSpeed / 10f, 0.5f, 2f);
            meteorPrefab.transform.localScale = new Vector3(scale, scale, 1);

            // Instantiate the meteor and set its initial velocity
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, spawnRotation);
            Rigidbody2D meteorRigidbody = meteor.GetComponent<Rigidbody2D>();
            meteorRigidbody.velocity = meteorDirection * meteorSpeed;

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
