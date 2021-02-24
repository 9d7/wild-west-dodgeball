using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Physics")]
    public Vector2 direction;
    public float speed;
    public float turnSpeed = 2f;
    public bool turning;

    protected float existTime = 10f;


    private Rigidbody2D rigid;



    // Start is called before the first frame update
    void Start()
    {
        InitObject();
    }

    protected void InitObject()
    {
        Vector2 velocityVector = direction * speed;
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = velocityVector;

        if (velocityVector != Vector2.zero)
        {
            rigid.SetRotation(Vector2.SignedAngle(Vector2.right, velocityVector));
        }
    }

    protected void HitPlayer(Collision2D other, float damage)
    {
        PlayerHealth ph = other.collider.GetComponentInParent<PlayerHealth>();
        if (ph)
        {
            ph.TakeDamage(damage);
        }
    }



    

}
