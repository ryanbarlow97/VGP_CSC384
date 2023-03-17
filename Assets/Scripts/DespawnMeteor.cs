using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnMeteor : MonoBehaviour
{
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    public float offset = 2f; // Add an offset value

    void Start()
    {
        mainCamera = Camera.main;
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        minX = screenBottomLeft.x - offset;
        maxX = screenTopRight.x + offset;
        minY = screenBottomLeft.y - offset;
        maxY = screenTopRight.y + offset;
    }

    void Update()
    {
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        minX = screenBottomLeft.x - offset;
        maxX = screenTopRight.x + offset;
        minY = screenBottomLeft.y - offset;
        maxY = screenTopRight.y + offset;
        if (transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY || transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }
}
