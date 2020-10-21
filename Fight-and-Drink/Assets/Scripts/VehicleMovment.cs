using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovment : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;
    public float TurningRate;
    public Rigidbody2D car; 

    /// <summary>
    /// Updates each frame and takes the input of the user to steer the car
    /// </summary>
    void FixedUpdate()
    {
        if(Input.GetKey("w"))
        {
            Accelerate();
        }
        else
        {
            Brake();
        }
        if (Input.GetKey("s"))
        {
            Reverse();
        }
        if (Input.GetKey("d"))
        {
            car.rotation += -TurningRate;
        }
        if (Input.GetKey("a"))
        {
            car.rotation += TurningRate;
        }
    }
    /// <summary>
    /// Adds force equal to the Acceleration to the vehicle if the current velocity is less than the MaxSpeed
    /// </summary>
    public void Accelerate()
    {
        if(MaxSpeed > car.velocity.magnitude)
        {
            car.AddForce(transform.up * Acceleration);
        }
    }
    /// <summary>
    /// Adds backwards force to the vehicle equal to the Acceleration * 0.7 
    /// </summary>
    public void Reverse()
    {
        car.AddForce(-transform.up * Acceleration * 0.7f);
    }
    /// <summary>
    /// Slows down the vehicle if the player does not accelerate
    /// </summary>
    public void Brake()
    {
        car.velocity = car.velocity * 0.95f;
    }

}
