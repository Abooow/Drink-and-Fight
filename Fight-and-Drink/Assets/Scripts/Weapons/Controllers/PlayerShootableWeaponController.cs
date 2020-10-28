using System.Collections;
using UnityEngine;

public class PlayerShootableWeaponController : PlayerWeaponController
{
    private GameObject weaponTrigger;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (transform.parent.tag == "Player")
        {
            weaponTrigger = transform.parent.Find("WeaponSoundAlert").gameObject;
            ShootableWeapon shootable = Weapon as ShootableWeapon;
            if (shootable != null)
            {
                shootable.OnWeaponFired += Shootable_OnWeaponFired;
            }
        }
    }

    private void Shootable_OnWeaponFired(IWeapon weapon, System.EventArgs args)
    {
        StartCoroutine("EnableGameObject");
    }

    private IEnumerator EnableGameObject()
    {
        weaponTrigger.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        weaponTrigger.SetActive(false);
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
