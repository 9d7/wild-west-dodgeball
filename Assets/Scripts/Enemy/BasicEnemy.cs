using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    [SerializeField] private float gunChance;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyShooting>().sequences.Add(
            this.BasicAttack
        );
    }

    IEnumerator BasicAttack (EnemyShooting shooter, Vector2 pos, Vector2 playerPos)
    {
        string projectile = Random.value < gunChance ? "gun" : "ball";
        shooter.Shoot(pos, (playerPos - pos).normalized, projectile);
        yield return null;
    }
}
