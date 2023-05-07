using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRate = 1f;
    private Camera mainCamera;
    public float spawnAngleRange = 40.0f;
    public GameObject playerShip;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
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

                    // Instantiate the meteor
                    GameObject meteor = Instantiate(meteorPrefab, spawnPosition, spawnRotation);

                    float scale = Random.Range(1f, 2f); // Random scale between 1 and 2
                    meteorPrefab.transform.localScale = new Vector3(scale, scale, 1); // Set the meteor's scale

                    // Calculate the meteor's velocity
                    Vector2 meteorDirection = GetSpawnDirection(spawnPosition);
                    Rigidbody2D meteorRigidbody = meteor.GetComponent<Rigidbody2D>();
                    meteorRigidbody.velocity = meteorDirection * Random.Range(1f, 4f);
                    
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
}
