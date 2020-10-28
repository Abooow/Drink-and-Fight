using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAnimation : MonoBehaviour
{
    private Rigidbody2D rg2d;
    private PoliceScript policeScript;
    private Animator animator;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        policeScript = GetComponent<PoliceScript>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (policeScript.MovingState)
        {
            case MovingState.Standing:
                animator.SetBool("Walking", false);
                break;
            case MovingState.Limping:
                break;
            case MovingState.Walking:
                animator.SetBool("Walking", true);
                break;
            case MovingState.Running:
                break;
            case MovingState.LimpingRun:
                break;
        }
    }
}
