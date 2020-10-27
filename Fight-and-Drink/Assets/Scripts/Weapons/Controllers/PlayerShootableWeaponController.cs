using UnityEngine;

public class PlayerShootableWeaponController : PlayerWeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Weapon.Attack();
        }
    }
}
