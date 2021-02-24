using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField] private float damage = 25;
    private void Start()
    {
        InitObject();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        HitPlayer(other, damage);
        Destroy(gameObject);
    }
}
