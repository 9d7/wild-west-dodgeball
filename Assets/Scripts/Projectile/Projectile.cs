using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Physics")]
    public Vector2 direction;
    public float speed;


    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {

        Vector2 velocityVector = direction * speed;
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = velocityVector;

        if (velocityVector != Vector2.zero)
        {
            rigid.SetRotation(Vector2.SignedAngle(Vector2.right, velocityVector));
        }
    }



    

}
