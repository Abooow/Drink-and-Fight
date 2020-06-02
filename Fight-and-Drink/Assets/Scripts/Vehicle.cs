using UnityEngine;

/// <summary>
/// class vehicle for cars in the game
/// </summary>
public class Vehicle : MonoBehaviour
{
    public float MaxVelocity;
    public float Acceleration;
    public float Weight;

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
}
