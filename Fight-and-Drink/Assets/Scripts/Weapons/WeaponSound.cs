using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    public static WeaponSound Instance;

    public List<AudioClip> WeaponSounds;
    public AudioSource AudioSource;
    private ShootableWeapon shootWeapon;
    private System.Random randomPunch;

    // Start is called before the first frame update
    void Start()
    {
        randomPunch = new System.Random();
        shootWeapon = GetComponent<ShootableWeapon>();
        shootWeapon.OnWeaponFired += ShootableWeapon_OnWeaponFired;
        shootWeapon.OnWeaponReload += ReloadWeapon_OnWeaponReload;
        Instance = this;
    }

    private void ReloadWeapon_OnWeaponReload(IWeapon weapon, EventArgs args)
    {
        AudioSource.clip = WeaponSounds[1];
        AudioSource.Play();
        Debug.Log("reload");
    }

    private void ShootableWeapon_OnWeaponFired(IWeapon weapon, System.EventArgs args)
    {
        if (WeaponSounds.Count == 3)
        {
             int punch = randomPunch.Next(0, 3);
            Debug.Log("punch " + punch);
            AudioSource.clip = WeaponSounds[punch];
            AudioSource.Play();
        }
        else
        {
            AudioSource.clip = WeaponSounds[0];
            AudioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
