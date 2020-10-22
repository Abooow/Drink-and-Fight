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
        //Checks the input to drive accordingly
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
            car.velocity = car.velocity * 0.97f;
        }
        if (Input.GetKey("a"))
        {
            car.rotation += TurningRate;
            car.velocity = car.velocity * 0.97f;
        }
        //Radio code
        if (inVehicle && Input.GetKeyDown(KeyCode.E))
        {
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
            player.transform.position = this.gameObject.transform.position;
            Radio.Instance.Pause();
        }
        if (inVehicle && Input.GetKeyDown(KeyCode.F1))
        {
            Radio.Instance.NextChannel();
        }
        if (inVehicle && Input.GetKeyDown(KeyCode.F4))
        {
            if (!pauseRadio)
            {
                Radio.Instance.Pause();
                pauseRadio = true;
            }
            else if (pauseRadio)
            {
                Radio.Instance.Resume();
                pauseRadio = false;
            }
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
     private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && inVehicle == false)
        {           
            OnEnterVehicle(collision);
        }
    }

    private void OnEnterVehicle(Collider2D collision)
    {
        Radio.Instance.Resume();
        inVehicle = true;
        player = collision.gameObject;
        Debug.Log("boom");
        collision.gameObject.SetActive(false);
        collision.transform.parent = this.gameObject.transform;
        vehicleScript.enabled = true; 
    }

}
