using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;

    public GameObject rightBooster;
    public GameObject leftBooster;
    public GameObject mainBooster;

    public AudioSource audioSource;
    public AudioClip thrustSound;

    private Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;

    void Start()
    {
        Camera camera = Camera.main;
        float cameraWidth = camera.orthographicSize * camera.aspect;
        screenWidth = cameraWidth;
        screenHeight = camera.orthographicSize;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 previousPosition = transform.position;


        if (vertical > 0)
        {
            Vector2 direction = transform.up;
            rb.velocity += direction * acceleration * Time.deltaTime;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        Vector2 newPosition = (Vector2)transform.position + (Vector2)rb.velocity * Time.deltaTime;
        newPosition.x = WrapValue(newPosition.x, -screenWidth, screenWidth);
        newPosition.y = WrapValue(newPosition.y, -screenHeight, screenHeight);
        transform.position = newPosition;

        if (horizontal < 0)
        {
            transform.Rotate(0, 0, -horizontal * rotationSpeed * Time.deltaTime);

            rightBooster.SetActive(true);
            leftBooster.SetActive(false);
        }
        else if (horizontal > 0)
        {
            transform.Rotate(0, 0, -horizontal * rotationSpeed * Time.deltaTime);

            rightBooster.SetActive(false);
            leftBooster.SetActive(true);
        }
        else
        {
            rightBooster.SetActive(false);
            leftBooster.SetActive(false);
        }

        if (previousPosition != (Vector2)transform.position && vertical > 0)
        {
            mainBooster.SetActive(true);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = thrustSound;
                audioSource.Play();
            }
        }
        else
        {
            mainBooster.SetActive(false);
            audioSource.Stop();
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
