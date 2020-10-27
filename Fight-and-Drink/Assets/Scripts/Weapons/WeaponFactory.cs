using UnityEngine;

/// <summary>
/// A weapon factory that have all the weapons in the game, used to easily get any weapon object.
/// </summary>
public class WeaponFactory : MonoBehaviour
{
    public static WeaponFactory Instance;

    public GameObject[] Weapons;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Get a weapon from the factory weapon collection, named "name".
    /// </summary>
    /// <param name="name">The name of the weapon to receive.</param>
    /// <returns>A weapon named name, null if nothing was found.</returns>
    public static GameObject GetWeapon(string name)
    {
        foreach (GameObject weaponObj in Instance.Weapons)
        {
            if (weaponObj.TryGetComponent(out IWeapon weapon) && weapon.Name == name) return weaponObj;
        }

        return null;
    }
}