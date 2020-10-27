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

    public float LimpingMultiplier = 0.5f;
    public float WalkingMultiplier = 1f;
    public float RunningMultiplier = 1.5f;

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
    Running,
    LimpingRun
}

