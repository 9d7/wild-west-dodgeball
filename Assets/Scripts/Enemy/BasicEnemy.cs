using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyShooting>().sequences.Add(
            this.BasicAttack
        );
    }

    IEnumerator BasicAttack (EnemyShooting shooter, Vector2 pos, Vector2 playerPos)
    {
        if (Random.value < 1f)
        {
            shooter.Shoot(pos, (playerPos - pos).normalized, "dodgeball");
        }
        else
        {
            shooter.Shoot(pos, (playerPos - pos).normalized, "gun");
        }
        
        yield return null;
    }
}
