using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Rigidbody2D rb;
    private float acceleration;
    private float maxSpeed;
    private Vector2 direction;
    public MoveCommand(Rigidbody2D rb, float acceleration, float maxSpeed, Vector2 direction)
    {
        this.rb = rb;
        this.acceleration = acceleration;
        this.maxSpeed = maxSpeed;
        this.direction = direction;
    }

    public void Execute()
    {
        rb.velocity = direction * acceleration;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}