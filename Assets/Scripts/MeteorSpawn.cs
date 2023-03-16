using System.Collections;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public float spawnRate = 1f;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = Camera.main;
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;

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

            switch (randomEdge)
            {
                case 0: // Top
                    spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1, mainCamera.nearClipPlane));
                    spawnRotation = Quaternion.Euler(0, 0, Random.Range(-30, 30));
                    break;
                case 1: // Bottom
                    spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 0, mainCamera.nearClipPlane));
                    spawnRotation = Quaternion.Euler(0, 0, Random.Range(150, 210));
                    break;
                case 2: // Left
                    spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(0, Random.Range(0f, 1f), mainCamera.nearClipPlane));
                    spawnRotation = Quaternion.Euler(0, 0, Random.Range(60, 120));
                    break;
                case 3: // Right
                    spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, Random.Range(0f, 1f), mainCamera.nearClipPlane));
                    spawnRotation = Quaternion.Euler(0, 0, Random.Range(240, 300));
                    break;
            }
            spawnPosition.z = 0f;

            Instantiate(meteorPrefab, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
