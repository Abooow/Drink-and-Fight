using UnityEngine;

/// <summary>
/// An abstract class that holds values related to the players movement.
/// </summary>
public abstract class CharacterMover : MonoBehaviour
{
    public float MaxStamina = 100;
    public float CurrentStamina = 100;
    public float Speed = WorldConstants.CharacterSpeed;
    public MovingState MovingState;
    public float LimpingMultiplier;
    public float WalkingMultiplier;
    public float SprintingMultiplier;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    virtual protected void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    virtual protected void Update()
    {
    }

    /// <summary>
    /// This gets and returns speed.
    /// </summary>
    /// <returns></returns>
    /// 
    public abstract float GetSpeed();
}

/// <summary>
/// Used to track the movement state of a character. 
/// </summary>
public enum MovingState
{
    Standing,
    Limping,
    Walking,
    Running
}
