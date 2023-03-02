using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lengthX, lengthY;
    public Camera cam;
    public float parallaxEffectX;
    public float parallaxEffectY;
    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the sprite
        startPosition = transform.position;

        // Get the length of the sprite on the x and y axis
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Calculate the distance of the camera on the x and y axis
        float tempX = cam.transform.position.x * (1 - parallaxEffectX);
        float distX = cam.transform.position.x * parallaxEffectX;
        float tempY = cam.transform.position.y * (1 - parallaxEffectY);
        float distY = cam.transform.position.y * parallaxEffectY;

        // Update the position of the sprite based on the camera distance and parallax effect
        transform.position = new Vector3(startPosition.x + distX, startPosition.y + distY, transform.position.z);

        // If the camera has moved beyond the bounds of the sprite on the x axis, update the starting position to move the sprite with the camera
        if (tempX > startPosition.x + lengthX) startPosition.x += lengthX;

        // If the camera has moved in the opposite direction, update the starting position accordingly
        else if (tempX < startPosition.x - lengthX) startPosition.x -= lengthX;

        // If the camera has moved beyond the bounds of the sprite on the y axis, update the starting position to move the sprite with the camera
        if (tempY > startPosition.y + lengthY) startPosition.y += lengthY;

        // If the camera has moved in the opposite direction, update the starting position accordingly
        else if (tempY < startPosition.y - lengthY) startPosition.y -= lengthY;
    }
}
