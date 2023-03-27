using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCommand : ICommand
{
    private GameObject gameObject;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private float offset;

    public DespawnCommand(GameObject gameObject, float offset = 2f)
    {
        this.gameObject = gameObject;
        this.offset = offset;
        mainCamera = Camera.main;

        UpdateScreenBounds();
    }

    public void Execute()
    {
        UpdateScreenBounds();

        if (gameObject.transform.position.x < minX || gameObject.transform.position.x > maxX || 
            gameObject.transform.position.y < minY || gameObject.transform.position.y > maxY)
        {
            Object.Destroy(gameObject);
        }
    }

    private void UpdateScreenBounds()
    {
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        minX = screenBottomLeft.x - offset;
        maxX = screenTopRight.x + offset;
        minY = screenBottomLeft.y - offset;
        maxY = screenTopRight.y + offset;
    }
}
