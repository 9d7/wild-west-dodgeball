using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public float moveAccel = 1;
    public float stopDistance;
    public float retreatDistance;

    public bool targetPlayer;
    private Transform player;

    public float startWaitTime;
    private float waitTime;

    public Transform[] moveSpots;
    private int randomSpot;
    private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        GameManager.Instance.RegisterEnemy(this);
        waitTime = startWaitTime;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
            wanderFunc();
        

    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemyDied(this);
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
        rbody.velocity = Vector3.MoveTowards(rbody.velocity, (moveSpots[randomSpot].position - transform.position).normalized * moveSpeed, moveAccel);
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
