using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Command interface
public interface ICommand
{
    void Execute();
}

// Move command
public class MoveCommand : ICommand
{
    private Rigidbody2D rb;
    private float acceleration;
    private float maxSpeed;
    private float vertical;

    public MoveCommand(Rigidbody2D rb, float acceleration, float maxSpeed, float vertical)
    {
        this.rb = rb;
        this.acceleration = acceleration;
        this.maxSpeed = maxSpeed;
        this.vertical = vertical;
    }

    public void Execute()
    {
        Vector2 direction = rb.transform.up;
        rb.velocity += direction * acceleration * Time.deltaTime;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}

// Rotate command
public class RotateCommand : ICommand
{
    private Transform transform;
    private float rotationSpeed;
    private float horizontal;

    public RotateCommand(Transform transform, float rotationSpeed, float horizontal)
    {
        this.transform = transform;
        this.rotationSpeed = rotationSpeed;
        this.horizontal = horizontal;
    }

    public void Execute()
    {
        transform.Rotate(0, 0, -horizontal * rotationSpeed * Time.deltaTime);
    }
}

// Activate booster command
public class ActivateBoosterCommand : ICommand
{
    private GameObject booster;

    public ActivateBoosterCommand(GameObject booster)
    {
        this.booster = booster;
    }

    public void Execute()
    {
        booster.SetActive(true);
    }
}

// Deactivate booster command
public class DeactivateBoosterCommand : ICommand
{
    private GameObject booster;

    public DeactivateBoosterCommand(GameObject booster)
    {
        this.booster = booster;
    }

    public void Execute()
    {
        booster.SetActive(false);
    }
}

// Play sound command
public class PlaySoundCommand : ICommand
{
    private AudioSource audioSource;
    private AudioClip sound;

    public PlaySoundCommand(AudioSource audioSource, AudioClip sound)
    {
        this.audioSource = audioSource;
        this.sound = sound;
    }

    public void Execute()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}

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
    public Camera camera;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;
    public float cameraSpeed = 0.1f;

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
        float cameraWidth = camera.orthographicSize * camera.aspect;
        screenWidth = cameraWidth;
        screenHeight = camera.orthographicSize;

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
            rb.velocity += direction * acceleration * Time.deltaTime;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }

        Vector2 newPosition = (Vector2)transform.position + (Vector2)rb.velocity * Time.deltaTime;

        newPosition.x = WrapValue(newPosition.x, -screenWidth+camera.transform.position.x, screenWidth+camera.transform.position.x);
        newPosition.y = WrapValue(newPosition.y, -screenHeight+camera.transform.position.y, screenHeight+camera.transform.position.y);

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
        Vector3 velocity = rb.velocity * cameraSpeed;
        Vector3 newCameraPosition = Camera.main.transform.position + velocity * Time.deltaTime;

        newCameraPosition.z = -20;
        Camera.main.transform.position = newCameraPosition;
    }
    
    float WrapValue(float value, float min, float max)
    {
        float range = max - min;
        while (value < min) value += range;
        while (value > max) value -= range;
        return value;
    }
}
