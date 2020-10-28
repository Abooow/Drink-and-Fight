using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovment : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;
    public float TurningRate;
    public Rigidbody2D car;
    private VehicleMovment vehicleScript;
    private GameObject player;
    public float CarIdle= 0.01f;
    public float CarDriftingSpeed = 0.8f;

    /// <summary>
    /// Called before the first frame update
    /// </summary>
    public void Start()
    {
        vehicleScript = GetComponent<VehicleMovment>();
        vehicleScript.enabled = false;
    }

    /// <summary>
    /// Updates each frame and takes the input of the user to steer the car
    /// </summary>
    void FixedUpdate()
    {
        checkIdleCar();
        //Checks the input to drive accordingly
        if (Input.GetKey("w"))
        {
            Accelerate();
            if (car.GetComponent<Rigidbody2D>().velocity.magnitude > 8.7 && Input.GetKey("w"))
            {
                CarSound.Instance?.PlayDrivingSound();
            }
            else if (car.GetComponent<Rigidbody2D>().velocity.magnitude < 8.7 && Input.GetKey("w"))
            {
                CarSound.Instance?.PlayCarAccelerateSound();
            }
        }
        else
        {
            Brake();
        }
        if (Input.GetKeyUp("w") && car.GetComponent<Rigidbody2D>().velocity.magnitude > 4)
        {
            CarSound.Instance?.PlayCarSlowdownSound();
        }
        if (Input.GetKey("s"))
        {
            if (car.GetComponent<Rigidbody2D>().velocity.magnitude > 3) { CarSound.Instance.PlayCarBreakSound(); Brake(); }
            else { Reverse(); CarSound.Instance.PlayCarAccelerateSound(); }
          
        }
        if (Input.GetKey("d"))
        {
            if (car.GetComponent<Rigidbody2D>().velocity.magnitude > CarDriftingSpeed)// CarSound.Instance?.PlayCarDriftingSound();
            car.rotation += -TurningRate;
            car.velocity = car.velocity * 0.97f;
        }
        if (Input.GetKey("a"))
        {
            if(car.GetComponent<Rigidbody2D>().velocity.magnitude > CarDriftingSpeed) //CarSound.Instance?.PlayCarDriftingSound();
            car.rotation += TurningRate;
            car.velocity = car.velocity * 0.97f;
        }
    }

    /// <summary>
    /// Checks if car is not moving
    /// </summary>
    private void checkIdleCar()
    {
        if(car?.GetComponent<Rigidbody2D>().velocity.magnitude < CarIdle)
        {
            CarSound.Instance?.PlayIdleCarSound();
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
