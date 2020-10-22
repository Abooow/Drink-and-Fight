using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeaponController : MonoBehaviour
{
    public IWeapon Weapon { get; set; }

    protected virtual void Start()
    {
        Weapon = gameObject.GetComponent<IWeapon>();
    }
}
