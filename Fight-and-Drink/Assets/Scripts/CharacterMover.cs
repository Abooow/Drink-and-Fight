using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A class that holds values related to the players movement
public class CharacterMover : MonoBehaviour
{
    
    public float MaxStamina { get; set; }
    public float CurrentStamina { get; set; }
    public float Speed { get; set; }
    public enum MovingState { }
    public float LimpingMultiplier { get; set; }
    public float WalkingMultiplier { get; set; }
    public float SprintingMultiplier { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returnerar speed
    public float GetSpeed()
    {
        return this.Speed;
    }
}

enum MovingState
{
    Standing,
    Limping,
    Walking,
    Running
}
