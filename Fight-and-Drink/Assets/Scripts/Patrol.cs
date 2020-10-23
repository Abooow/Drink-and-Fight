using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform[] moveSpots;
    public int moveSpot;
    private int randomSpot;

    void Start()
    {
        waitTime = startWaitTime;
        moveSpot = 0;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[moveSpot].position, speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[moveSpot].position) < 0.2f)
        {
            if(waitTime <= 0){
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;

                moveSpot += 1;
                if(moveSpot >= moveSpots.Length)
                {
                    moveSpot = 0;
                }

            }else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
