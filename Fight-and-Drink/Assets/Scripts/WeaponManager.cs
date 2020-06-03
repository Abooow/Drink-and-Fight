﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managers all the weapons a character can hold.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    public GameObject CurrentWeapon;

    public int TotalWeapons => weapons.Count;

    private List<GameObject> weapons;
    private int weaponIndex;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        if (weapons == null)
        {
            weapons = new List<GameObject>()
            {
                null // null = punch.
            };
        }
        else weapons.Insert(0, null);
    }

    /// <summary>
    /// Adds a weapon to the weapon list, if the weapon already exists, then the ammo from the newWeaponObj will be added onto the existing weapon.
    /// </summary>
    /// <param name="newWeaponObj">The new weapon to add.</param>
    public void AddWeapon(GameObject newWeaponObj)
    {
        if (newWeaponObj == null) return;

        if (!newWeaponObj.TryGetComponent(out IWeapon newWeapon)) return;

        // Adds the ammo from newWeaponObj to the existing weapon if the weapon already exists.
        bool weaponExisted = false;
        foreach (GameObject weaponObj in weapons)
        {
            if (weaponObj == null || !newWeaponObj.TryGetComponent(out IWeapon weapon)) continue;

            if (weapon.Name == newWeapon.Name)
            {
                if (weapon is ShootableWeapon shootable && newWeapon is ShootableWeapon newShootable)
                {
                    shootable.TotalBullets += newShootable.CurrentBullets + newShootable.TotalBullets;
                }
                else if (weapon is ThrowableWeapon throwable && newWeapon is ThrowableWeapon newThrowable)
                {
                    throwable.CurrentThrowables += newThrowable.CurrentThrowables;
                }

                weaponExisted = true;
                break;
            }
        }

        // Adds and instantiates a new weapon to the weapon list if the weapon didn't exist.
        if (!weaponExisted)
        {
            for (int i = 0; i < TotalWeapons + 1; i++)
            {
                if (i == TotalWeapons)
                {
                    weapons.Add(newWeaponObj);
                    break;
                }

                if (weapons[i] == null) continue;

                if (newWeapon.OrderIndex > weapons[i].GetComponent<IWeapon>().OrderIndex)
                {
                    weapons.Insert(i, newWeaponObj);
                    break;
                }
            }

            Instantiate(newWeaponObj, transform).SetActive(false);
        }
    }

    /// <summary>
    /// Removes a weapon named "name" from the weapon list.
    /// </summary>
    /// <param name="weaponName">The name of the weapon to remove.</param>
    public void RemoveWeapon(string weaponName)
    {
        for (int i = TotalWeapons - 1; i >= 0; i--)
        {
            if (weapons[i].TryGetComponent(out IWeapon weapon) && weapon.Name == weaponName)
            {
                weapons.Remove(weapons[i]);
                if (CurrentWeapon == weapons[i]) NextWeapon();
                Destroy(weapons[i]);

                break;
            }
        }
    }

    /// <summary>
    /// Changes the current weapon to the next weapon in the weapon list.
    /// </summary>
    public void NextWeapon()
    {
        SetWeapon(++weaponIndex);
    }

    /// <summary>
    /// Changes the current weapon to the previous weapon in the weapon list.
    /// </summary>
    public void PreviousWeapon()
    {
        SetWeapon(--weaponIndex);
    }

    /// <summary>
    /// Changes the current weapon to the weapon at index "index" in the weapon list.
    /// </summary>
    /// <param name="index">The index to use when changing weapon.</param>
    public void SetWeapon(int index)
    {
        if (index < 0) index = TotalWeapons - 1;
        else if (index >= TotalWeapons) index = 0;

        CurrentWeapon?.SetActive(false);

        weaponIndex = index;
        CurrentWeapon = weapons[weaponIndex];

        CurrentWeapon?.SetActive(true);
    }
}
