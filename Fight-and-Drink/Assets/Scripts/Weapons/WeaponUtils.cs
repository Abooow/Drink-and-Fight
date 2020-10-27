using System;
using UnityEngine;

public delegate void WeaponEvent(IWeapon weapon, EventArgs args);

public class ShotEvent : EventArgs
{
    public Vector2 ShotDirection { get; }

    public ShotEvent(Vector2 shotDirection)
    {
        ShotDirection = shotDirection;
    }
}

public class ThrownEvent : EventArgs
{
    public Vector2 ThrowDirection { get; }
    public float ThrowDistance { get; }
    public GameObject ThrowObject { get; set; }

    public ThrownEvent(Vector2 throwDirection, float throwDistance, GameObject throwObject)
    {
        ThrowDirection = throwDirection;
        ThrowDistance = throwDistance;
        ThrowObject = throwObject;
    }
}