using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private static Vector2 direction;
    private float lengthX, lengthY;
    public float parallaxEffectX;
    public float parallaxEffectY;
    private Vector3 startPosition;

    private Transform playerTransform;
    private Vector3 previousPlayerPosition;

    void Start()
    {
        // Store the starting position of the sprite
        startPosition = transform.position;

        // Get the length of the sprite on the x and y axis
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;

        // Set the direction if it hasn't been set yet
        if (direction == Vector2.zero)
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        // Find the player object and store its transform component
        playerTransform = GameObject.FindGameObjectWithTag("PlayerShip").transform;
        previousPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        // Calculate the player's movement direction based on the difference between its current and previous positions
        Vector3 playerMovement = playerTransform.position - previousPlayerPosition;
        Vector2 playerDirection = new Vector2(playerMovement.x, playerMovement.y).normalized;

        // Update the direction gradually based on the player's movement direction
        direction = Vector2.Lerp(direction, -playerDirection, 0.5f * Time.deltaTime);

        // Update the position of the sprite based on the direction and parallax effect
        transform.position += new Vector3(direction.x * Time.deltaTime * parallaxEffectX, direction.y * Time.deltaTime * parallaxEffectY, 0f);

        // If the sprite has moved beyond the bounds of the background, reset its position
        if (Mathf.Abs(transform.position.x - startPosition.x) > lengthX)
        {
            transform.position = new Vector3(startPosition.x, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(transform.position.y - startPosition.y) > lengthY)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
        }

        // Store the player's current position for the next frame
        previousPlayerPosition = playerTransform.position;
    }
}
