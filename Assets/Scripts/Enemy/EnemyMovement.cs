using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    public int health = 1;
    public float moveSpeed;
    public float moveAccel = 1;
    public float stopDistance;
    public float retreatDistance;
    public float attackRange = 5;

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

    public int randomSeed;
    private Vector3 nextMovingSpot;

    
    public GameObject enemyZone;

    private EnemySpawn enemySpawn;
    private MainMenu menuController;

    enum enemy_state
    {
        IDLE,
        PATROL,
        ALERT,
        ATTACK
    }
    enemy_state state = enemy_state.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        menuController = GameObject.FindObjectOfType<MainMenu>();
        enemySpawn = GameObject.FindObjectOfType<EnemySpawn>();
        rbody = GetComponent<Rigidbody2D>();
        GameManager.Instance.RegisterEnemy(this);
        waitTime = startWaitTime;
        attackTime = attackInterval + Random.Range(-(attackInterval)/4, (attackInterval)/4);
        player = GameObject.FindWithTag("Player").transform;
        Random.InitState(randomSeed);
        //nextMovingSpot = moveSpots[Random.Range(0, moveSpots.Length)].position;
        state = enemy_state.ATTACK;
        StartCoroutine(DoAttackLoop());
    }
    private void Update()
    {
        /*Debug.Log(Vector2.Distance(transform.position, player.position));
        if (state == enemy_state.IDLE)
        {
            //state = enemy_state.PATROL;
            if (Vector2.Distance(transform.position, player.position) < attackRange)
            {
                state = enemy_state.PATROL;
            }
        }
        else if (state == enemy_state.PATROL)
        {
            if (attackTime <= Random.value)
            {
                attackTime = attackInterval + Random.Range(-(attackInterval)/4, (attackInterval)/4);
                StartCoroutine(AttackReady());

            }
            attackTime -= Time.deltaTime;
            if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                attackTime = attackInterval + Random.Range(-(attackInterval)/4, (attackInterval)/4);
                state = enemy_state.IDLE;
            }
        }*/
    }

    IEnumerator DoAttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval + Random.Range(-(attackInterval) / 4, (attackInterval) / 4));
            BasicAttack(gameObject.transform.position, player.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        randomWalk();
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
        
        rbody.velocity = Vector3.MoveTowards(rbody.velocity, (nextMovingSpot - transform.position).normalized * moveSpeed, moveAccel);
        if(Vector2.Distance(transform.position, nextMovingSpot) < 0.1f){
            rbody.velocity = new Vector3(0, 0, 0);
            if (waitTime <= 0)
            {
                int randomSpot_tmp = Random.Range(0, 100) % moveSpots.Length;
                /*
                while (randomSpot_tmp == randomSpot)
                {
                    randomSpot_tmp = Random.Range(0, moveSpots.Length);
                }
                */
                randomSpot = randomSpot_tmp;
                //Vector3 potNextSpot = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
                nextMovingSpot = transform.position + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(-5, 5));
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

    private Vector3 walkdirection;
    void randomWalk()
    {
        
        if (waitTime <= 0)
        {
            waitTime = startWaitTime;
            walkdirection = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0).normalized;
            
        } else
        {
            waitTime -= Time.deltaTime;
        }
        rbody.velocity = Vector3.MoveTowards(rbody.velocity, walkdirection * moveSpeed, moveAccel);
    }

    IEnumerator AttackReady()
    {
        state = enemy_state.ATTACK;
        //yield return new WaitForSeconds(0.2f);
        BasicAttack(gameObject.transform.position, player.position);
        yield return new WaitForSeconds(0.1f);
        state = enemy_state.PATROL;
    }

    void Shoot(Vector2 position, Vector2 direction, string type)
    {

        GameObject newProj = Instantiate(
            projectileTypes[type]?.template,
            position,
            Quaternion.identity
        );
        newProj.GetComponent<Projectile>().direction = direction;
        newProj.GetComponent<Projectile>().speed = 12;
        SendMessage("EnemyShoot");
        //newProj.layer = LayerMask.NameToLayer("ProjectileFromEnemy");
    }

    void BasicAttack(Vector2 pos, Vector2 playerPos)
    {
        Vector2 enemyToPlayer = (playerPos - pos).normalized;
        pos = pos + enemyToPlayer;
        if (this.tag == "boss")
        {
            Shoot(pos, enemyToPlayer + new Vector2(0.5f, 0.5f), "gun");
            Shoot(pos, enemyToPlayer - new Vector2(0.5f, 0.5f), "gun");
        }
        else
        {
            if (Random.value < 0.5f)
            {
                Shoot(pos, enemyToPlayer, "bottle");
            }
            else
            {
                Shoot(pos, enemyToPlayer, "barrel");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.layer == LayerMask.NameToLayer("ProjectileFromAlly"))
        {
            Debug.Log("took hit");
            health -= 1;
            if(health <= 0)
            {
                SendMessage("EnemyDie");
                Destroy(enemyZone);
                enemySpawn.enemyDied();
                if(gameObject.tag == "boss")
                {
                    menuController.GameEnd(true);
                }
            }
        }
    }
}
