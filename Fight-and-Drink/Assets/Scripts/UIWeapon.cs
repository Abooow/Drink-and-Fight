using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
    public WeaponManager WeaponManager;
    public Image WeaponImage;
    public Text Text;
    public List<KeyValueWeapon> Weapons;

    private ShootableWeapon shootableWeapon;
    private ThrowableWeapon throwableWeapon;

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
        if (shootableWeapon != null)
        {
            Text.text = $"{shootableWeapon.CurrentBullets}/{shootableWeapon.TotalBullets}";
        }
        else if (throwableWeapon != null)
        {
            Text.text = $"{throwableWeapon.CurrentThrowables}";
        }
        else
        {
            Text.text = "";
        }
    }

    private void OnWeaponChanged(IWeapon weapon, System.EventArgs args)
    {
        if (weapon == null)
        {
            WeaponImage.sprite = Weapons[0].Value;
            return;
        }

        if (weapon is ShootableWeapon shootable)
        {
            shootableWeapon = shootable;
            throwableWeapon = null;
        }
        else if (weapon is ThrowableWeapon throwable)
        {
            shootableWeapon = null;
            throwableWeapon = throwable;
        }
        else
        {
            shootableWeapon = null;
            throwableWeapon = null;
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