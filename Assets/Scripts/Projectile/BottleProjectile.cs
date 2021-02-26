using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleProjectile : Projectile
{

    //[SerializeField] private float turnSpeed = 2f;

    private float timer = 0f;

    private float secondsPerShot;
    // Start is called before the first frame update
    void Start()
    {
        InitObject();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.Rotate(0f, 0f, Time.deltaTime * turnSpeed * 180f / Mathf.PI);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HitPlayer(other, 10f);
        Destroy(gameObject);
    }
}