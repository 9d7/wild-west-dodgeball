using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgebal : MonoBehaviour
{
    public ParticleSystem trail;
    public LayerMask ignoreLayers;
    private bool active = true;
    private Rigidbody2D rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (rbody.velocity.magnitude > 1.0f)
        {
            BasicEnemy ph = other.collider.GetComponentInParent<BasicEnemy>();
            if (ph)
            {
                Destroy(ph.gameObject);
                Destroy(gameObject);
            }
            trail.Stop();
            GetComponent<Rigidbody2D>().drag = 2;
        }
        
    }
    
    
}