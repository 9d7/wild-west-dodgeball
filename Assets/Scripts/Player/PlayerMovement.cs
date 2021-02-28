using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [SerializeField] private Transform showBallParent;
    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject dodgeball;

    [Header("Animation")]
    [SerializeField] private Animator spriteAnim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private GameObject ballInHand;
    private Vector2 aimingDir;

    
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
        PickupRange = 1.75f;
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
        Collider2D dodgeball = Physics2D.OverlapCircle(transform.position, PickupRange, (int)PickupLayer);
        if (dodgeball)
        {
            if (dodgeball.tag == "Catchable")
            {
                Destroy(dodgeball.gameObject);
                ballInHand = dodgeball.gameObject;
                Sprite ball = dodgeball.GetComponent <SpriteRenderer>().sprite;
                PickupBall(ball);
            }
        }
    }

    void OnShoot(InputValue val)
    {
        if (!hasBall)
            return;
        ThrowBall();

    }

    void OnAim(InputValue input)
    {
        Ray r = playerCam.ScreenPointToRay(input.Get<Vector2>());
        Plane groundPlane = new Plane(Vector3.forward, 0);
        float hitT = 0;
        groundPlane.Raycast(r, out hitT);
        Vector3 targetPos = r.origin + hitT * r.direction;
        aimingDir = (targetPos - transform.position).normalized;
    }

    void PickupBall(Sprite ball)
    {
        hasBall = true;
        showBall.sprite = ball;
        showBall.enabled = true;
    }

    void ThrowBall()
    {
        hasBall = false;
        showBall.enabled = false;
        GameObject newDodgeball = GameObject.Instantiate(dodgeball);
        newDodgeball.transform.position = transform.position + (Vector3)(aimingDir * 0.2f);
        newDodgeball.GetComponent<Rigidbody2D>().velocity = aimingDir * 50;
    }

    void OnMove(InputValue input)
    {
        Vector2 lastMovement = movementInput;
        movementInput = input.Get<Vector2>();
        if(movementInput.x != 0)
        {
            spriteRenderer.flipX = movementInput.x < 0;
        }

        if(movementInput.magnitude > 0.1)
        {
            spriteAnim.SetFloat("WalkY", (movementInput.y + 1) / 2);
        }

        spriteAnim.SetBool("Walking", movementInput.magnitude > 0.1);

    }

    // Runs on every physics timestep
    private void FixedUpdate()
    {
        if (dashing)
            return;
        Vector2 targetVel = movementInput * Speed;
        Debug.Log("Running");
        rigid.velocity = Vector3.MoveTowards(rigid.velocity, targetVel, Acceleration);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        spriteAnim.SetBool("Dashing", true);
        rigid.velocity = movementInput * (Speed * 2f);
        yield return new WaitForSeconds(DashTime);
        dashing = false;
        spriteAnim.SetBool("Dashing", false);
        yield return new WaitForSeconds(DashRecoveryTime);
        canDash = true;
    }

    private void Update()
    {
        if (hasBall)
        {
            showBallParent.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, aimingDir) * Mathf.Sign(Vector3.Cross(Vector3.up, aimingDir).z));
        }
    }
}
