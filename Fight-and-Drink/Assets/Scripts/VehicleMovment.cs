using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovment : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;
    public float TurningRate;
    public Rigidbody2D car; 

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
        if (Input.GetKey("d"))
        {
            car.rotation += -TurningRate;
        }
        if (Input.GetKey("a"))
        {
            car.rotation += TurningRate;
        }
    }
    public void Accelerate()
    {
        car.AddForce(transform.up*Acceleration);
        Debug.Log("BRUMM");
    }
    public void Brake()
    {
        car.velocity = car.velocity * 0.95f;
    }

}
