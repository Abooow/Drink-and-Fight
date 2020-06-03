using UnityEngine;

/// <summary>
/// Defines a weapon that will shoot (throw) other gameObjects that inherit the IThrowableObject interface.
/// </summary>
public class ThrowableWeapon : MonoBehaviour, IWeapon
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public float FireRate { get; set; }

    public GameObject ThrowObject;
    public float MaxThrowDistance;
    public int CurrentThrowables;

    private float throwTimer;
    private bool canThrow;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        canThrow = true;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (!canThrow)
        {
            throwTimer += Time.deltaTime;
            if (throwTimer >= FireRate)
            {
                throwTimer = 0;
                canThrow = true;
            }
        }
    }

    /// <summary>
    /// Shoots (throws) the ThrowObject forward with a default distance of 10.
    /// </summary>
    public void Shoot()
    {
        Shoot(10);
    }

    /// <summary>
    /// Shoots (throws) the ThrowObject forward with a specified distance.
    /// </summary>
    /// <param name="distance">How far to throw the object, can also be seen as strength.</param>
    public void Shoot(float distance)
    {
        if (!canThrow || CurrentThrowables == 0 || ThrowObject == null) return;

        if (distance > MaxThrowDistance) distance = MaxThrowDistance;

        GameObject gameObject = Instantiate(ThrowObject, transform.position, transform.rotation);
        if (gameObject.TryGetComponent(typeof(IThrowableObject),  out _))
        {
            if (gameObject.TryGetComponent(out Rigidbody2D rigidbody)) rigidbody.AddForce(transform.forward * distance);
        }
        else DestroyImmediate(gameObject);
    }
}