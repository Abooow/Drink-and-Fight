using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerWeapons : MonoBehaviour
{
    public WeaponManager PlayerWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerWeaponManager.AddWeapon(WeaponFactory.GetWeapon("Gun 1"));
        PlayerWeaponManager.AddWeapon(WeaponFactory.GetWeapon("Gun 2"));
        PlayerWeaponManager.AddWeapon(WeaponFactory.GetWeapon("Gun 3"));
        PlayerWeaponManager.AddWeapon(WeaponFactory.GetWeapon("Gun 4"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerWeaponManager.PreviousWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerWeaponManager.NextWeapon();
        }
    }
}
