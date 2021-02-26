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

    private float time = 0;

    public ProjectileTypes projectileTypes;

    public float attackInterval = 5;
    private float attackTime;

    enum enemy_state
    {
        IDLE,
        ALERT,
        ATTACK
    }
    enemy_state state = enemy_state.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        GameManager.Instance.RegisterEnemy(this);
        waitTime = startWaitTime;
        attackTime = attackInterval;
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        Random.InitState((int)(Random.value * 100));
        if (attackTime <= Random.value)
        {
            attackTime = attackInterval;
            StartCoroutine(AttackReady());

        }
        attackTime -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == enemy_state.IDLE)
        {
            //wanderFunc();
            lissajous_curve(5, 2, 5, 4, Mathf.PI / 4);
        } else if (state == enemy_state.ATTACK)
        {
            rbody.velocity = new Vector3(0, 0, 0);
        }

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

    void lissajous_curve(float A, float B, float a, float b, float e)
    {
        time += Time.deltaTime;
        float x = A * Mathf.Sin(a * Mathf.PI * time * 0.1f + e + Random.value);
        float y = B * Mathf.Sin(b * Mathf.PI * time * 0.1f + Random.value);
        Vector2 move_path = new Vector2(x, y);
        rbody.velocity = move_path;
    }

    IEnumerator AttackReady()
    {
        state = enemy_state.ATTACK;
        yield return new WaitForSeconds(0.2f);
        BasicAttack(gameObject.transform.position, player.position);
        yield return new WaitForSeconds(0.6f);
        state = enemy_state.IDLE;
    }

    void Shoot(Vector2 position, Vector2 direction, string type)
    {
        GameObject newProj = Instantiate(
            projectileTypes[type]?.template,
            position,
            Quaternion.identity
        );
        newProj.GetComponent<Projectile>().direction = direction;
        //newProj.layer = LayerMask.NameToLayer("ProjectileFromEnemy");
    }

    void BasicAttack(Vector2 pos, Vector2 playerPos)
    {
        if (Random.value < 0.8f)
        {
            Shoot(pos, (playerPos - pos).normalized, "bottle");
        }
        else if (Random.value < 0.9f)
        {
            Shoot(pos, (playerPos - pos).normalized, "gun");
        }
        else
        {
            Shoot(pos, (playerPos - pos).normalized, "dodgeball2");
        }
    }
}
