using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour, IWeapon
{
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int OrderIndex { get => _orderIndex; set => _orderIndex = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public bool DrawGizmos { get => _drawGizmos; set => _drawGizmos = value; }

    public Transform WeaponMuzzle;
    public float MaxBulletDistance;
    private float fireTimer;
    private Vector2 lastShotDirection;
    public float Damage;
    private Rigidbody2D R2d;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _orderIndex;
    [SerializeField] private float _fireRate;
    [SerializeField] private bool _canAttack;
    [SerializeField] private bool _drawGizmos;

    void start()
    {
        _canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (!_canAttack)
        {
            // FireRate timer.
            fireTimer += Time.deltaTime;
            if (fireTimer >= FireRate)
            {
                fireTimer = 0;
                _canAttack = true;
            }
        }

        float startAngle = transform.eulerAngles.z;
        Vector2 rayDir = new Vector2();
        lastShotDirection = rayDir;

        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(WeaponMuzzle.position, rayDir, MaxBulletDistance))
        {
            if (hit.transform.gameObject.TryGetComponent(out Damageable damageable)) damageable.TakeDamage(Damage);

            var punchedObj = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            if (punchedObj != null)
            {
                punchedObj.AddForce(10 * transform.up, ForceMode2D.Impulse);
            }
        }

        _canAttack = false;
    }

    public void HandleAddWeapon(IWeapon other)
    {
        
    }
    public void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.DrawRay(WeaponMuzzle.position, lastShotDirection * MaxBulletDistance);
        }
    }
}
