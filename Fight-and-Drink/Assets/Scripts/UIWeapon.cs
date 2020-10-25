using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
    public WeaponManager WeaponManager;
    public Image WeaponImage;
    public List<KeyValueWeapon> Weapons;

    // Start is called before the first frame update
    void Start()
    {
        WeaponManager.OnWeaponChanged += OnWeaponChanged;
       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnWeaponChanged(IWeapon weapon)
    {
        WeaponImage = GetComponent<Image>();

        if (weapon.Name == null)
        {
            WeaponImage.sprite = Weapons[0].Value;
        }
        if (weapon.Name == Weapons[1].Key)
        {
            WeaponImage.sprite = Weapons[1].Value;
        }

    }
}

[System.Serializable]
public class KeyValueWeapon
{
    public string Key;
    public Sprite Value;

    public KeyValueWeapon(string key, Sprite value)
    {
        Key = key;
        Value = value;
    }


}