using UnityEngine;

/// <summary>
/// Defines a weapon that will shoot (throw) other gameObjects that inherit the IThrowableObject interface.
/// </summary>
public class ThrowableWeapon : MonoBehaviour, IWeapon
{
    public event WeaponEvent OnObjectThrown;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int OrderIndex { get => _orderIndex; set => _orderIndex = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    public GameObject ThrowObject;
    public float MaxThrowDistance;
    public int CurrentThrowables;

    private float throwTimer;
    private bool canThrow;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _orderIndex;
    [SerializeField] private float _fireRate;
    [SerializeField] private bool _canAttack;

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

        GameObject gameObj = Instantiate(ThrowObject, transform.position, transform.rotation);
        if (gameObj.TryGetComponent(typeof(IThrowableObject),  out _))
        {
            if (gameObj.TryGetComponent(out Rigidbody2D rigidbody)) rigidbody.AddForce(transform.forward * distance);
        }
        else DestroyImmediate(gameObj);

        OnObjectThrown?.Invoke(this, new ThrownEvent(transform.forward, distance, ThrowObject));
    }

    [System.Obsolete("Use Shoot() instead")]
    public void Attack()
    {
        Shoot();
    }

    public void HandleAddWeapon(IWeapon other)
    {
        if (other is ThrowableWeapon throwable)
            CurrentThrowables += throwable.CurrentThrowables;
    }
}