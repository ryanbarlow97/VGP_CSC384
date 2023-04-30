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
    public Camera newCamera;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;

    private ICommand moveCommand;
    private ICommand rotateCommand;
    private ICommand activateMainBoosterCommand;
    private ICommand activateLeftBoosterCommand;
    private ICommand activateRightBoosterCommand;
    private ICommand deactivateMainBoosterCommand;
    private ICommand deactivateLeftBoosterCommand;
    private ICommand deactivateRightBoosterCommand;
    private ICommand playThrustSoundCommand;

    void Start()
    {
        float cameraWidth = newCamera.orthographicSize * newCamera.aspect;
        screenWidth = cameraWidth;
        screenHeight = newCamera.orthographicSize;

        rb = GetComponent<Rigidbody2D>();

        activateLeftBoosterCommand = new ActivateBoosterCommand(leftBooster);
        activateRightBoosterCommand = new ActivateBoosterCommand(rightBooster);
        activateMainBoosterCommand = new ActivateBoosterCommand(mainBooster);
        deactivateLeftBoosterCommand = new DeactivateBoosterCommand(leftBooster);
        deactivateRightBoosterCommand = new DeactivateBoosterCommand(rightBooster);
        deactivateMainBoosterCommand = new DeactivateBoosterCommand(mainBooster);
        playThrustSoundCommand = new PlaySoundCommand(audioSource, thrustSound);
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 previousPosition = transform.position;

        rotateCommand = new RotateCommand(transform, rotationSpeed, horizontal);

        if (vertical > 0)
        {
            Vector2 direction = transform.up;  
            moveCommand = new MoveCommand(rb, acceleration * Time.deltaTime, maxSpeed, direction);

            moveCommand.Execute();
        }


        Vector2 newPosition = (Vector2)transform.position + (Vector2)rb.velocity * Time.deltaTime;

        newPosition.x = WrapValue(newPosition.x, 
            -screenWidth+newCamera.transform.position.x, 
            screenWidth+newCamera.transform.position.x);

        newPosition.y = WrapValue(newPosition.y, 
            -screenHeight+newCamera.transform.position.y, 
            screenHeight+newCamera.transform.position.y);

        transform.position = newPosition;

        if (horizontal < 0)
        {
            rotateCommand.Execute();
            activateRightBoosterCommand.Execute();
            deactivateLeftBoosterCommand.Execute();
            

        }
        else if (horizontal > 0)
        {
            rotateCommand.Execute();
            deactivateRightBoosterCommand.Execute();
            activateLeftBoosterCommand.Execute();
        }
        else
        {
            deactivateRightBoosterCommand.Execute();
            deactivateLeftBoosterCommand.Execute();
        }

        if (previousPosition != (Vector2)transform.position && vertical > 0)
        {
            activateMainBoosterCommand.Execute();

            playThrustSoundCommand.Execute();
        }
        else
        {
            deactivateMainBoosterCommand.Execute();
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
