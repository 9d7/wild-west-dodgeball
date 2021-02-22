using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public float stopDistance;
    public float retreatDistance;

    public bool targetPlayer;
    private Transform player;

    public float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer)
        {
            targetPlayerFunc();
        } else
        {
            //wander
            wanderFunc();
        }

    }

    void targetPlayerFunc()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stopDistance &&
          Vector2.Distance(transform.position, player.position) > retreatDistance
          )
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        }
    }

    void wanderFunc()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, moveSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
            if(waitTime <= 0)
            {
                int randomSpot_tmp = Random.Range(0, moveSpots.Length);
                while(randomSpot_tmp == randomSpot)
                {
                    randomSpot_tmp = Random.Range(0, moveSpots.Length);
                }
                randomSpot = randomSpot_tmp;
                waitTime = startWaitTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
