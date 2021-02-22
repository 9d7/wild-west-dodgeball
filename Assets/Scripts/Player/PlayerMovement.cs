using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Options")] [SerializeField] private float Speed;
    [SerializeField] private float Acceleration;

    [SerializeField] private float DashTime;

    [SerializeField] private float DashRecoveryTime;
    [SerializeField] private float PickupRange;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private SpriteRenderer showBall;

    
    // Start is called before the first frame update
    private Vector2 movementInput;

    private bool invincible = false;

    private bool hasBall = false;

    private Animator anim;

    private bool dashing = false;
    private bool canDash = true;
    

    private Rigidbody2D rigid;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        showBall.enabled = false;
    }

    void OnDash()
    {
        if (!canDash)
        {
            return;
        }
        StartCoroutine(Dash());
    }

    void OnGrab(InputValue input)
    {
        if (dashing)
            return;
        Debug.Log("Grabbing");
        Collider2D dodgeball = Physics2D.OverlapCircle(transform.position, PickupRange, (int)PickupLayer);
        Debug.Log(dodgeball);
        if (dodgeball)
        {
            Destroy(dodgeball.gameObject);
        }
    }

    void PickupBall()
    {
        hasBall = true;
        showBall.enabled = true;
    }

    void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    // Runs on every physics timestep
    private void FixedUpdate()
    {
        if (dashing)
            return;
        Vector2 targetVel = movementInput * Speed;

        rigid.velocity = Vector3.MoveTowards(rigid.velocity, targetVel, Acceleration);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        rigid.velocity = movementInput * (Speed * 2f);
        anim.SetBool("Dashing", true);
        yield return new WaitForSeconds(DashTime);
        anim.SetBool("Dashing", false);
        dashing = false;
        yield return new WaitForSeconds(DashRecoveryTime);
        canDash = true;
    }
}
