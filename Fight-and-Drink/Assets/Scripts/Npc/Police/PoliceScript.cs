using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceScript : CharacterMover
{
    public PoliceState PoliceState 
    { 
        get => _policeState; 
        set
        {
            _policeState = value;
            ChangeState(_policeState);
        }
    }
    public Rigidbody2D Rigidbody;

    private PolicePassiveState passiveState;
    private PoliceAggressiveState aggressiveState;
    [SerializeField] private PoliceState _policeState;

    protected override void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        passiveState = GetComponent<PolicePassiveState>();
        aggressiveState = GetComponent<PoliceAggressiveState>();

        PoliceState = _policeState;
    }

    protected override void Update()
    {
        
    }

    public void ChangeState(PoliceState newState)
    {
        _policeState = newState;

        switch (newState)
        {
            case PoliceState.Passive:
                passiveState.enabled = true;
                aggressiveState.enabled = false;
                break;
            case PoliceState.Aggressive:
                passiveState.enabled = false;
                aggressiveState.enabled = true;
                break;
            case PoliceState.Seraching:
                break;
        }
    }

    public override float GetSpeed()
    {
        switch (MovingState)
        {
            case MovingState.Limping:
                return Speed * LimpingMultiplier;
            case MovingState.Walking:
                return Speed * WalkingMultiplier;
            case MovingState.Running:
                return Speed * RunningMultiplier;
            case MovingState.LimpingRun:
                return Speed * LimpingMultiplier * RunningMultiplier;
            default:
                return 0;
        }
    }
}

public enum PoliceState
{
    Passive,
    Aggressive,
    Seraching
}