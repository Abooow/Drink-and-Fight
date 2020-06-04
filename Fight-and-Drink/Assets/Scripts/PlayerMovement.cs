using UnityEngine;

/// <summary>
/// Controls the Players movement.
/// </summary>
public class PlayerMovement : CharacterMover
{
    private Rigidbody2D rb2d;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    protected override void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    protected override void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection == Vector2.zero) MovingState = MovingState.Standing;
        else if (Input.GetKey(KeyCode.LeftShift)) MovingState = MovingState.Running;
        else MovingState = MovingState.Walking;

        transform.Translate(moveDirection * GetSpeed() * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Get the speed of the player depending on the MovingState.
    /// </summary>
    /// <returns>The wanted speed based on the players state.</returns>
    public override float GetSpeed()
    {
        switch (MovingState)
        {
            case MovingState.Limping:
                return Speed * LimpingMultiplier;
            case MovingState.Walking:
                return Speed * WalkingMultiplier;
            case MovingState.Running:
                return Speed * RunningMultiplier;
            case MovingState.LimpingRun:
                return Speed * LimpingMultiplier * RunningMultiplier;
            default:
                return 0;
        }
    }
}

