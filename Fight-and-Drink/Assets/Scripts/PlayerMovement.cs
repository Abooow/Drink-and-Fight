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

        rb2d.AddForce(moveDirection * GetSpeed() * Time.deltaTime);
    }

    public override float GetSpeed()
    {
        return this.Speed;
    }
}
