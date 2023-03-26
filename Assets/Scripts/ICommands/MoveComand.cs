using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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