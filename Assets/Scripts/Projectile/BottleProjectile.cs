using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleProjectile : Projectile
{

    //[SerializeField] private float turnSpeed = 2f;

    private float timer = 0f;

    private float secondsPerShot;

    [SerializeField] private GameObject shatterEffect;
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
        if (state == projState.INIT)
        {
            timeActivate -= Time.deltaTime;
            if (timeActivate <= 0)
            {
                state = projState.INTHEAIR;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
        if (state == projState.INTHEAIR)
        {
            Destroy(GameObject.Instantiate(shatterEffect, transform.position, shatterEffect.transform.rotation), 1);
            HitPlayer(other, 10f);
            Destroy(gameObject);
        }
    }
}
