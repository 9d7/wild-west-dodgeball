using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : Projectile
{

    //[SerializeField] private float turnSpeed = 2f;
    [SerializeField] private float shotsPerTurn = 5f;
    [SerializeField] private GameObject bullet;

    private float timer = 0f;

    private float secondsPerShot;
    // Start is called before the first frame update
    void Start()
    {
        InitObject();
        secondsPerShot = Mathf.PI * 2f / (turnSpeed * shotsPerTurn);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        gameObject.transform.Rotate(0f, 0f, Time.deltaTime * turnSpeed * 180f/Mathf.PI);

        if (gameObject.layer == LayerMask.NameToLayer("ProjectileFromEnemy"))
        {
            if (timer > secondsPerShot)
            {
                timer -= secondsPerShot;
                Vector3 bulletPos = gameObject.transform.position;
                GameObject newBullet = Instantiate(bullet, bulletPos, Quaternion.identity);

                newBullet.layer = gameObject.layer;
                Projectile proj = newBullet.GetComponent<Projectile>();
                proj.direction = gameObject.transform.rotation * Vector3.right;

            }
        }
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
        if (state == projState.INTHEAIR)
        {
            Destroy(gameObject);
        }
    }
}
