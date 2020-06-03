using UnityEngine;

/// <summary>
/// Defines a weapon that will shoot bullets (raycasts).
/// </summary>
public class ShootableWeapon : MonoBehaviour, IWeapon
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public float FireRate { get; set; }

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
        if (!canShoot || !isReloading || CurrentBullets <= 0) return;

        float offset = Random.Range(-BulletSpread, BulletSpread);
        float startAngle = transform.rotation.z;
        Vector2 rayDir = new Vector2(Mathf.Cos(startAngle + offset), Mathf.Sin(startAngle + offset));

        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(WeaponMuzzle.position, rayDir, MaxBulletDistance))
        {
            if (hit.transform.gameObject.TryGetComponent(out Damageable damageable)) damageable.TakeDamage(Damage);
        }

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
}