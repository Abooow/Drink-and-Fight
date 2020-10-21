using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovment : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;
    public Rigidbody2D car; 

    void FixedUpdate()
    {
        if(Input.GetKey("w"))
        {
            Accelerate();
        }
    }
    public void Accelerate()
    {
        car.AddForce(transform.up*Acceleration);
        Debug.Log("BRUMM");
    }

}
