using UnityEngine;

/// <summary>
/// Defines a weapon that will shoot bullets (raycasts).
/// </summary>
public class ShootableWeapon : MonoBehaviour, IWeapon
{
    public static int RotationOffset = 90;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int OrderIndex { get => _orderIndex; set => _orderIndex = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }

    public Transform WeaponMuzzle;
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
    private bool canShoot;

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
        canShoot = true;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (!canShoot)
        {
            if (!isReloading)
            {
                // FireRate timer.
                fireTimer += Time.deltaTime;
                if (fireTimer >= FireRate)
                {
                    fireTimer = 0;
                    canShoot = true;
                }
            }
            else
            {
                // Reloading timer.
                reloadTimer += Time.deltaTime;
                if (reloadTimer >= ReloadSpeed)
                {
                    isReloading = false;
                    canShoot = true;
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
        if (!canShoot || isReloading) return;
        if (CurrentBullets <= 0)
        {
            Reload();
            return;
        }

        float offset = Random.Range(-BulletSpread, BulletSpread) + RotationOffset;
        float startAngle = transform.eulerAngles.z;
        Vector2 rayDir = new Vector2(Mathf.Cos((startAngle + offset) * 0.0174532925f), Mathf.Sin((startAngle + offset) * 0.0174532925f));

        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(WeaponMuzzle.position, rayDir, MaxBulletDistance))
        {
            if (hit.transform.gameObject.TryGetComponent(out Damageable damageable)) damageable.TakeDamage(Damage);
        }

        canShoot = false;
        CurrentBullets--;
        if (CurrentBullets <= 0) Reload();
    }

    /// <summary>
    /// Prepares this weapon to be reloaded.
    /// </summary>
    public void Reload()
    {
        if (TotalBullets <= 0) return;

        canShoot = false;
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
}