using UnityEngine;

/// <summary>
/// An object that can psychically be thrown.
/// </summary>
public interface IThrowableObject
{
    GameObject GameObject { get; set; }
    float Damage { get; set; }
}
