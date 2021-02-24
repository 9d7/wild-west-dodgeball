using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    public ParticleSystem trail;
    public LayerMask ignoreLayers;
    private bool active = false;
    private float timeActivate = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeActivate -= Time.deltaTime;
        if (timeActivate <= 0)
        {
            active = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (active && (((1 << other.gameObject.layer) & (int)ignoreLayers) == 0))
        {
            PlayerHealth ph = other.collider.GetComponentInParent<PlayerHealth>();
            if (ph)
            {
                ph.TakeDamage(Mathf.Infinity);
            }
            active = false;
            trail.Stop();
            GetComponent<Rigidbody2D>().drag = 2;
        }
        
    }
    
    
}
