using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAggressiveState : MonoBehaviour
{
    public float MinShootDistance = 5f;
    public float MinFollowDistance = 1f;
    public float MaxDistance = 9f;

    private PoliceScript policeScript;
    private WeaponManager weaponManager;
    private Rigidbody2D rb2d;
    private IWeapon weapon;
    private float attackTimer;

    void Start()
    {
        policeScript = GetComponent<PoliceScript>();
        weaponManager = GetComponent<WeaponManager>();
        rb2d = GetComponent<Rigidbody2D>();

        weaponManager.AddWeapon(WeaponFactory.GetWeapon("Gun 1"));
        weaponManager.NextWeapon();
        weapon = weaponManager.CurrentWeapon.GetComponent<IWeapon>();
    }

    void Update()
    {
        float distanceToPlayer = (transform.position - GameManager.Instance.Player.transform.position).magnitude;

        if (distanceToPlayer >= MinFollowDistance)
        {
            policeScript.MovingState = MovingState.Walking;
            rb2d.velocity = (GameManager.Instance.Player.transform.position - transform.position).normalized * policeScript.GetSpeed();
        }
        else
        {
            policeScript.MovingState = MovingState.Standing;
            rb2d.velocity = Vector2.zero;
        }

        if (distanceToPlayer >= MaxDistance)
            policeScript.ChangeState(PoliceState.Passive);
        
        if (attackTimer >= 0.8f && distanceToPlayer <= MinShootDistance)
        {
            attackTimer = 0f;
            weapon.Attack();
        }
        attackTimer += Time.deltaTime;

        HandleLookAt();
    }

    private void HandleLookAt()
    {
        transform.up = Vector3.Lerp(transform.up, (GameManager.Instance.Player.transform.position - transform.position).normalized, 0.3f);
    }
}
