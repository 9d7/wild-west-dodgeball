using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : Projectile
{
    public ParticleSystem trail;
    public LayerMask ignoreLayers;
    


    
    // Start is called before the first frame update
    void Start()
    {
        InitObject();
        existTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (state == projState.BOUNCE)
        {
            existTime -= Time.deltaTime;
            if(existTime <= 0)
            {
                Destroy(gameObject);
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
        base.OnCollisionEnter2D(other);
        if (state == projState.INTHEAIR)
        {
            state = projState.BOUNCE;
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
