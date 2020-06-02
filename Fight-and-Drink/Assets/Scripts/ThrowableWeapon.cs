using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ThrowableWeapon : MonoBehaviour, IWeapon
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float FireRate { get; set; }

    public GameObject ThrowObject;
    public float MaxThrowDistance;
    public int CurrentThrowables;

    private float throwTimer;
    private bool canThrow;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        canThrow = true;
    }

    /// <summary>
    /// Update is called once per frame
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
    /// 
    /// </summary>
    public void Shoot()
    {
        Shoot(10);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distance"></param>
    public void Shoot(float distance)
    {
        if (CurrentThrowables == 0 || !canThrow) return;

        if (distance > MaxThrowDistance) distance = MaxThrowDistance;

        GameObject gameObject = Instantiate(ThrowObject, transform.position, transform.rotation);
        if (gameObject.TryGetComponent(out Rigidbody2D rigidbody)) rigidbody.AddForce(transform.forward * distance);
    }
}
