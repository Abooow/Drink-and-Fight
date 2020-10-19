using UnityEngine;

/// <summary>
/// Controls the Players movement.
/// </summary>
public class PlayerMovement : CharacterMover
{
    public Transform LookAtTarget;
    public float RotationOffset = -90f;

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
        HandleLookAt();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection == Vector2.zero) MovingState = MovingState.Standing;
        else if (Input.GetKey(KeyCode.LeftShift)) MovingState = MovingState.Running;
        else MovingState = MovingState.Walking;

        transform.Translate(moveDirection * GetSpeed() * Time.deltaTime, Space.World);
    }

    private void HandleLookAt() 
    {
        Vector2 lookAtPosition;
        if (LookAtTarget == null)
        {
            lookAtPosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        }
        else
        {
            lookAtPosition = LookAtTarget.position;
        }

        float angle = Mathf.Atan2(lookAtPosition.y, lookAtPosition.x) * Mathf.Rad2Deg + RotationOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

