using UnityEngine;

public class PlayerPunchController : PlayerWeaponController
{
    private Animator animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SpriteRenderer meele = GetComponent<SpriteRenderer>();
        meele.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SpriteRenderer meele = this.GetComponent<SpriteRenderer>();
            meele.enabled = true;
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("Idle", false);
            anim.SetBool("Punch", true);
            Weapon.Attack();
        }
        else
        {
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("Idle", true);
            anim.SetBool("Punch", false);
        }
    }
}
