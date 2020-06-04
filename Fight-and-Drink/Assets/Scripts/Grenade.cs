using UnityEngine;

/// <summary>
/// Class grenade is used by grenades in the game
/// </summary>
public class Grenade : MonoBehaviour, IThrowableObject
{
    public float ExplodeRadius;
    public float Damage { get; set; }

    /// <summary>
    /// This method gets called when the grenade explodes
    /// </summary>
    public void Explode()
    {
    }
}
