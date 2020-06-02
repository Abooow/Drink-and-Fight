using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ShootableWeapon : MonoBehaviour, IWeapon
{
    public string Name { get; set; }
    public string Description { get; set; }
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
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        canShoot = true;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (!canShoot)
        {
            if (!isReloading)
            {
                fireTimer += Time.deltaTime;
                if (fireTimer >= FireRate)
                {
                    fireTimer = 0;
                    canShoot = true;
                }
            }
            else
            {
                reloadTimer += Time.deltaTime;
                if (reloadTimer >= ReloadSpeed)
                {
                    isReloading = false;
                    canShoot = true;
                    fireTimer = 0;
                    reloadTimer = 0;

                    if (TotalBullets - MagazineCapacity < 0)
                    {
                        CurrentBullets = TotalBullets;
                        TotalBullets = 0;
                    }
                    else
                    {
                        CurrentBullets = MagazineCapacity;
                        TotalBullets -= MagazineCapacity;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    public void Reload()
    {
        if (TotalBullets <= 0) return;

        canShoot = false;
        isReloading = true;
    }
}

class Damageable // Remove!
{
    public void TakeDamage(float damage)
    {

    }
}
