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

    // using a coroutine because some bullet sequences
    // can take time, and we want to wait until those
    // are over to continue execution
    IEnumerator Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        while (true)
        {
            if (sequences.Count > 0)
            {
                BulletSequence attack = sequences[Random.Range(0, sequences.Count)];
                yield return StartCoroutine(
                    attack(this, gameObject.transform.position, player.position)
                );
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
    }

    public void Shoot(Vector2 position, Vector2 direction, string type)
    {
        GameObject newProj = Instantiate(
            projectileTypes[type]?.template,
            position,
            Quaternion.identity
        );
        newProj.GetComponent<Projectile>().direction = direction;
        newProj.layer = LayerMask.NameToLayer("ProjectileFromEnemy");
    }

}
