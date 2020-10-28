using UnityEngine;

/// <summary>
/// Defines a weapon that will shoot bullets (raycasts).
/// </summary>
public class ShootableWeapon : MonoBehaviour, IWeapon, IDrawGizmos
{
    public static int RotationOffset = 90;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int OrderIndex { get => _orderIndex; set => _orderIndex = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public bool DrawGizmos { get => _drawGizmos; set => _drawGizmos = value; }

    public ParticleSystem WeaponMuzzle;
    public float MaxBulletDistance;
    public int MagazineCapacity;
    public int CurrentBullets;
    public int TotalBullets;
    public float ReloadSpeed;
    public float BulletSpread;
    public float Damage;

    private float reloadTimer;
    private float fireTimer;
    private bool isReloading;
    private Vector2 lastShotDirection;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _orderIndex;
    [SerializeField] private float _fireRate;
    [SerializeField] private bool _canAttack;
    [SerializeField] private bool _drawGizmos;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        _canAttack = true;
        WeaponMuzzle.Stop();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (!_canAttack)
        {
            if (!isReloading)
            {
                // FireRate timer.
                fireTimer += Time.deltaTime;
                if (fireTimer >= FireRate)
                {
                    fireTimer = 0;
                    _canAttack = true;
                }
            }
            else
            {
                // Reloading timer.
                reloadTimer += Time.deltaTime;
                if (reloadTimer >= ReloadSpeed)
                {
                    isReloading = false;
                    _canAttack = true;
                    fireTimer = 0;
                    reloadTimer = 0;

                    if (TotalBullets - MagazineCapacity < 0)
                    {
                        // Use remaining ammo.
                        CurrentBullets = TotalBullets;
                        TotalBullets = 0;
                    }
                    else
                    {
                        // Fill magazine to full.
                        int newFill = MagazineCapacity - CurrentBullets;
                        CurrentBullets += newFill;
                        TotalBullets -= newFill;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Shoots a raycast forward from the WeaponMuzzle position.
    /// </summary>
    public void Shoot()
    {
        if (!_canAttack || isReloading) return;
        if (CurrentBullets <= 0)
        {
            Reload();
            return;
        }

        float offset = Random.Range(-BulletSpread, BulletSpread) + RotationOffset;
        float startAngle = transform.eulerAngles.z;
        Vector2 rayDir = new Vector2(Mathf.Cos((startAngle + offset) * 0.0174532925f), Mathf.Sin((startAngle + offset) * 0.0174532925f));
        lastShotDirection = rayDir;

        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(WeaponMuzzle.transform.position, rayDir, MaxBulletDistance))
        {
            if (hit.transform.gameObject.TryGetComponent(out Damageable damageable)) damageable.TakeDamage(Damage);

            var ObjHit = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            if(ObjHit != null)
            {
                ObjHit.AddForce(5 * transform.up, ForceMode2D.Impulse);
            }
        }

        _canAttack = false;
        CurrentBullets--;
        if (CurrentBullets <= 0) Reload();

        WeaponMuzzle.Play();
    }

    /// <summary>
    /// Prepares this weapon to be reloaded.
    /// </summary>
    public void Reload()
    {
        if (TotalBullets <= 0) return;

        _canAttack = false;
        isReloading = true;
    }

    [System.Obsolete("Use Shoot() instead")]
    public void Attack()
    {
        Shoot();
    }

    public void HandleAddWeapon(IWeapon other)
    {
        if (other is ShootableWeapon shootable)
            TotalBullets += shootable.CurrentBullets + shootable.TotalBullets;
    }

    public void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            if (!CanAttack && !isReloading)
            {
                Gizmos.DrawRay(WeaponMuzzle.transform.position, lastShotDirection * MaxBulletDistance);
            }
        }
    }
}