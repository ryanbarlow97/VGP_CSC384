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
        float vertical = Input.GetAxis("Vertical");

        Vector3 previousPosition = transform.position;

        if (vertical > 0)
        {
            Vector3 newPosition = transform.position + transform.up * vertical * speed * Time.deltaTime;
            newPosition.x = WrapValue(newPosition.x, -screenWidth, screenWidth);
            newPosition.y = WrapValue(newPosition.y, -screenHeight, screenHeight);

            transform.position = newPosition;
        }

        if (horizontal < 0)
        {
            transform.Rotate(0, 0, -horizontal * speed * Time.deltaTime * 25);

            rightBooster.SetActive(true);
            leftBooster.SetActive(false);
        }
        else if (horizontal > 0)
        {
            transform.Rotate(0, 0, -horizontal * speed * Time.deltaTime * 25);

            rightBooster.SetActive(false);
            leftBooster.SetActive(true);
        }
        else
        {
            rightBooster.SetActive(false);
            leftBooster.SetActive(false);
        }

        if (previousPosition != transform.position)
        {
            mainBooster.SetActive(true);
        }
        else
        {
            mainBooster.SetActive(false);
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
