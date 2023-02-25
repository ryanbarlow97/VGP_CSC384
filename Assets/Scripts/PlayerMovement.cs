using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;

    private float screenWidth;
    private float screenHeight;

    public GameObject rightBooster;
    public GameObject leftBooster;
    public GameObject mainBooster;

    void Start()
    {
        Camera camera = Camera.main;
        float cameraWidth = camera.orthographicSize * camera.aspect;
        screenWidth = cameraWidth;
        screenHeight = camera.orthographicSize;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        transform.Rotate(0, 0, -horizontal * speed * Time.deltaTime * 25);

        if (horizontal < 0)
        {
            rightBooster.SetActive(true);
            leftBooster.SetActive(false);
            mainBooster.SetActive(false);
        }
        else if (horizontal > 0)
        {
            rightBooster.SetActive(false);
            leftBooster.SetActive(true);
            mainBooster.SetActive(false);
        }

        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            Vector3 newPosition = transform.position + transform.up * vertical * speed * Time.deltaTime;
            newPosition.x = WrapValue(newPosition.x, -screenWidth, screenWidth);
            newPosition.y = WrapValue(newPosition.y, -screenHeight, screenHeight);

            transform.position = newPosition;

            rightBooster.SetActive(false);
            leftBooster.SetActive(false);
            mainBooster.SetActive(true);
        }
    }

    float WrapValue(float value, float min, float max)
    {
        float range = max - min;
        while (value < min) value += range;
        while (value > max) value -= range;
        return value;
    }
}
