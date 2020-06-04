using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator PlayerAnimator;
    private float horizontalInput;
    private float verticalInput;
    public bool moving;
    public bool dead;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    protected void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    protected void Update()
    {
        GetInput();
        
        if (horizontalInput != 0 || verticalInput != 0)
            moving = true;
        else
        {
            moving = false;
        }

        if (moving)
            MovingAnimation();
        else
            IdleAnimation();

        if (dead)
            DeadAnimation();
    }

    /// <summary>
    /// GetInput checks if player is moving
    /// </summary>
    private protected void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// Changes the parameters in the animator 
    /// </summary>
    private protected void MovingAnimation()
    {
        PlayerAnimator.SetBool("PlayerMoving", true);
        PlayerAnimator.SetBool("PlayerIdle", false);
        PlayerAnimator.SetBool("PlayerDead", false);
    }

    /// <summary>
    /// Changes the parameters in the animator 
    /// </summary>
    private protected void IdleAnimation()
    {
        PlayerAnimator.SetBool("PlayerIdle", true);
        PlayerAnimator.SetBool("PlayerMoving", false);
        PlayerAnimator.SetBool("PlayerDead", false);
    }

    /// <summary>
    /// Changes the parameters in the animator 
    /// </summary>
    private protected void DeadAnimation()
    {
        PlayerAnimator.SetBool("PlayerIdle", false);
        PlayerAnimator.SetBool("PlayerMoving", false);
        PlayerAnimator.SetBool("PlayerDead", true);
    }
}
