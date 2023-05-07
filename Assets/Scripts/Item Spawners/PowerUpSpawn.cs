using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject[] powerupPrefabs;
    public float spawnRate = 5f;
    private Camera mainCamera;
    public float spawnAngleRange = 40.0f;
    public GameObject playerShip;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            if (playerShip.GetComponent<TimeRewind>().isRewinding == false)
            {

                Vector3 spawnPosition = Vector3.zero;
                Quaternion spawnRotation = Quaternion.identity;

                // Choose a random edge: 0 = Top, 1 = Bottom, 2 = Left, 3 = Right
                int randomEdge = Random.Range(0, 4);

                float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
                float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;
                float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;
                float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)).y;

                switch (randomEdge)
                {
                    case 0: // Top
                        spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenTop, 0f);
                        break;
                    case 1: // Bottom
                        spawnPosition = new Vector3(Random.Range(screenLeft, screenRight), screenBottom, 0f);
                        break;
                    case 2: // Left
                        spawnPosition = new Vector3(screenLeft, Random.Range(screenBottom, screenTop), 0f);
                        break;
                    case 3: // Right
                        spawnPosition = new Vector3(screenRight, Random.Range(screenBottom, screenTop), 0f);
                        break;
                }

                // Choose a random powerup to instantiate
                GameObject powerupPrefab = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];

                // Instantiate the powerup
                GameObject powerup = Instantiate(powerupPrefab, spawnPosition, spawnRotation);

                // Calculate the powerup's velocity
                Vector2 powerupDirection = GetSpawnDirection(spawnPosition);
                Rigidbody2D powerupRigidbody = powerup.GetComponent<Rigidbody2D>();
                powerupRigidbody.velocity = powerupDirection * Random.Range(0.2f, 1f);

            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    Vector2 GetSpawnDirection(Vector3 spawnPosition)
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector3 screenCenterWorld = mainCamera.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, 0));
        Vector2 directionToCenter = (screenCenterWorld - spawnPosition).normalized;

        float randomAngle = Random.Range(-spawnAngleRange / 2, spawnAngleRange / 2);
        float angleInRadians = (randomAngle + Vector2.SignedAngle(Vector2.right, directionToCenter)) * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }
}
