using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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