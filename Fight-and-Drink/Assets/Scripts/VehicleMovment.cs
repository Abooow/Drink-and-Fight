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
    private bool inVehicle = false;
    private bool pauseRadio = false;
    private VehicleMovment vehicleScript;
    private GameObject player;
    public float CarIdle= 0.1f;
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
        //Debug.Log(car.GetComponent<Rigidbody2D>().velocity.magnitude);
        checkIdleCar();
        //Checks the input to drive accordingly
        if (Input.GetKey("w"))
        {
            Accelerate();
            if (car.GetComponent<Rigidbody2D>().velocity.magnitude > 10 && Input.GetKey("w"))
            {
                CarSound.Instance?.PlayDrivingSound();
            }
            else if (car.GetComponent<Rigidbody2D>().velocity.magnitude < 10 && Input.GetKey("w"))
            {
                CarSound.Instance?.PlayCarAccelerateSound();
            }
        }
        else
        {
            Brake();
        }
        if (Input.GetKeyUp("w")&& car.GetComponent<Rigidbody2D>().velocity.magnitude > 5)
        {
            CarSound.Instance?.PlayCarSlowdownSound();
        }
        if (car.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1)
        {
            // CarSound.Instance?.PlayIdleCarSound();
        }
        if (Input.GetKey("s"))
        {
            CarSound.Instance.PlayCarBreakSound();
            Reverse();
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
       // CarSound.Instance?.PlayCarBreakSound();
    }

    /// <summary>
    /// Slows down the vehicle if the player does not accelerate
    /// </summary>
    public void Brake()
    {
        //CarSound.Instance?.PlayCarSlowdownSound();
        car.velocity = car.velocity * 0.95f;
    }

}
