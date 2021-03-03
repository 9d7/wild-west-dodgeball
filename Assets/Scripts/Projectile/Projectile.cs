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
    public AudioClip collideSound;
    [SerializeField] private GameObject tempSound;
    public bool thrownByPlayer;


    private Rigidbody2D rigid;

    protected enum projState
    {
        INIT,
        INTHEAIR,
        BOUNCE
    }
    protected projState state = projState.INIT;
    protected float timeActivate = 0.1f;

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
        if (thrownByPlayer)
            return;
        var ph = other.collider.GetComponentInParent<PlayerHealth>();
        if (ph)
        {
            Debug.Log("TOOK DAMAGE FROM BOTTLE");
            ph.TakeDamage(damage);
        }
    }

    protected void OnCollisionEnter2D(Collision2D _)
    {
             GameObject newSound = Instantiate(tempSound, transform.position, Quaternion.identity);
             TemporaryAudio temp = newSound.GetComponent<TemporaryAudio>();
             temp.audio = collideSound;
             temp.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
             temp.volume = 1f;
             temp.destination = "Environment";
    }





}
