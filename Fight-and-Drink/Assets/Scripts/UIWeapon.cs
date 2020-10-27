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

        if(WeaponManager.CurrentWeapon == null)
        {
            WeaponImage.sprite = Weapons[0].Value;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnWeaponChanged(IWeapon weapon)
    {
        if (weapon == null)
        {
            WeaponImage.sprite = Weapons[0].Value;
            return;
        }


        for (int i = 0; i < Weapons.Count; i++)
        {
            if (weapon.Name == Weapons[i].Key)
            {
                WeaponImage.sprite = Weapons[i].Value;
                break;
            }
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