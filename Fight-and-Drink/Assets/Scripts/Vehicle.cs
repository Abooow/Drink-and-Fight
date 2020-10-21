using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// class vehicle for cars in the game
/// </summary>
public class Vehicle : MonoBehaviour
{
    public float MaxVelocity;
    public float Acceleration;
    public float Weight;
    private bool inVehicle = false;
    private VehicleMovment vehicleScript;
    private GameObject player;


    public void Start()
    {
        vehicleScript = GetComponent<VehicleMovment>();
        
        vehicleScript.enabled = false;
    }

    void Update()
    {
        if (inVehicle == true && Input.GetKey(KeyCode.E))
        {
            Debug.Log("EXIT");
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
            player.transform.position = this.gameObject.transform.position;
            Radio.Instance.Pause();
        }
         if(inVehicle == true && Input.GetKey(KeyCode.F1)) 
        {
            Debug.Log("INSIDE f2");
            Radio.Instance.NextChannel();
        }
   
    }

    /// <summary>
    /// function for accelerating the car
    /// </summary>
    virtual protected void Accelerate()
    {
    }

    /// <summary>
    /// function for slowing down the car
    /// </summary>
    virtual protected void Brake()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && Input.GetKey(KeyCode.F) && inVehicle == false)
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
