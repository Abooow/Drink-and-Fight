using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePassiveState : MonoBehaviour
{
    public float CircleRadius = 3f;
    public float MaxTime = 3f;
    public float MinDistance = 0.2f;

    private PoliceScript policeScript;
    private Vector2 targetPosition;
    private float timer;

    private void Start()
    {
        policeScript = GetComponent<PoliceScript>();
        FindNewTargetPosition();
        policeScript.MovingState = MovingState.Walking;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= MaxTime || (targetPosition - (Vector2)transform.position).magnitude <= MinDistance)
        {
            timer = 0;
            FindNewTargetPosition();
        }

        policeScript.MovingState = MovingState.Walking;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        policeScript.Rigidbody.velocity = direction * policeScript.GetSpeed();

        HandleLookAt();
    }

    private void HandleLookAt()
    {
        transform.up = Vector3.Lerp(transform.up, ((Vector3)targetPosition - transform.position).normalized, 0.3f);
    }

    private void FindNewTargetPosition()
    {
        targetPosition = Random.insideUnitCircle * CircleRadius + (Vector2)transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            policeScript.ChangeState(PoliceState.Aggressive);
        }
    }
}
