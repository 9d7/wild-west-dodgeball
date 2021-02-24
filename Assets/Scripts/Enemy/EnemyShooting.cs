using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private ProjectileTypes projectileTypes;
    [SerializeField] private float timeBetweenAttacks;

    public delegate IEnumerator BulletSequence (EnemyShooting shooter, Vector2 pos, Vector2 playerPos);
    public List<BulletSequence> sequences = new List<BulletSequence>();

    private Transform player;

    public float attackInterval = 5;
    private float attackTime;

    // using a coroutine because some bullet sequences
    // can take time, and we want to wait until those
    // are over to continue execution
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        attackTime = attackInterval;

        /*
        while (true)
        {
            if (sequences.Count > 0)
            {
                Debug.Log(sequences.Count);
                BulletSequence attack = sequences[Random.Range(0, sequences.Count)];
                yield return StartCoroutine(
                    attack(this, gameObject.transform.position, player.position)
                );
            }
            Debug.Log(timeBetweenAttacks);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        */
        
    }

    private void Update()
    {
        if (attackTime <= 0)
        {
            attackTime = attackInterval;
            BasicAttack(this, gameObject.transform.position, player.position);
        }
        attackTime -= Time.deltaTime;
    }

    public void Shoot(Vector2 position, Vector2 direction, string type)
    {
        GameObject newProj = Instantiate(
            projectileTypes[type]?.template,
            position,
            Quaternion.identity
        );
        newProj.GetComponent<Projectile>().direction = direction;
            //newProj.layer = LayerMask.NameToLayer("ProjectileFromEnemy");
    }

    void BasicAttack(EnemyShooting shooter, Vector2 pos, Vector2 playerPos)
    {
        if (Random.value < 0.5f)
        {
            shooter.Shoot(pos, (playerPos - pos).normalized, "dodgeball2");
        }
        else
        {
            shooter.Shoot(pos, (playerPos - pos).normalized, "gun");
        }
    }

}
