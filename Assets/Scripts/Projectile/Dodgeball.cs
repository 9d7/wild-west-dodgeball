using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : Projectile
{
    public ParticleSystem trail;
    public LayerMask ignoreLayers;
    private float timeActivate = 0.5f;

    enum dodgeballState
    {
        INIT,
        INTHEAIR,
        BOUNCE
    }
    private dodgeballState state = dodgeballState.INIT;
    
    // Start is called before the first frame update
    void Start()
    {
        InitObject();
        existTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (state == dodgeballState.BOUNCE)
        {
            existTime -= Time.deltaTime;
            if(existTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (state == dodgeballState.INIT)
        {
            timeActivate -= Time.deltaTime;
            if (timeActivate <= 0)
            {
                state = dodgeballState.INTHEAIR;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (state == dodgeballState.INTHEAIR)
        {
            state = dodgeballState.BOUNCE;
            PlayerHealth ph = other.collider.GetComponentInParent<PlayerHealth>();
            if (ph)
            {
                ph.TakeDamage(Mathf.Infinity);
            }
            trail.Stop();
            GetComponent<Rigidbody2D>().drag = 2;
        }  
    }

}
