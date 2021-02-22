using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float Damage = 25;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerHealth ph = coll.collider.GetComponentInParent<PlayerHealth>();
        if (ph)
        {
            ph.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
