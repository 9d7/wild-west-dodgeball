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
    [SerializeField] private float DashMultiplier = 2;
    
    [Header("Animation")]
    [SerializeField] public Animator spriteAnim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public ProjectileTypes projectileTypes;

    [Header("UI")] [SerializeField] private InventoryUI _inventoryUI;

    private GameObject ballInHand;
    private Vector2 aimingDir;

    
    // Start is called before the first frame update
    private Vector2 movementInput;

    private bool invincible = false;

    private bool hasBall = false;

    private Animator anim;

    private bool dashing = false;
    private bool canDash = true;

    private string catchedBall = "";

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
        Collider2D dodgeball = Physics2D.OverlapCircle(transform.position, PickupRange, (int)PickupLayer);
        //Debug.Log(dodgeball);
        
        spriteAnim.SetTrigger("Grab");
        StartCoroutine(Grab());
        if (dodgeball)
        {
            catchedBall = dodgeball.tag;
            Debug.Log(catchedBall);
            Destroy(dodgeball.gameObject);
            ballInHand = dodgeball.gameObject;
            Sprite ball = dodgeball.GetComponent <SpriteRenderer>().sprite;
            _inventoryUI.PickUp(dodgeball.tag);
            PickupBall(ball, dodgeball.transform);
        }
    }

    void OnPause()
    {
        GameManager.Instance.TogglePause();
    }

    void OnShoot(InputValue val)
    {
        spriteAnim.SetTrigger("Throw");
        ThrowBall();

    }

    void OnInventory(InputValue val)
    {
        //Debug.Log("something");
        if (val.Get<float>() > 0)
        {
            _inventoryUI.UpSlot();
            showBall.sprite = _inventoryUI.GetCurrentSprite();
        }
        else if (val.Get<float>() < 0)
        {
            _inventoryUI.DownSlot();
            showBall.sprite = _inventoryUI.GetCurrentSprite();
        }
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

    void PickupBall(Sprite ball, Transform t)
    {
        showBall.sprite = ball;
        showBall.enabled = true;
        Transform oldParent = showBall.transform.parent;
        showBall.transform.SetParent(null);
        showBall.transform.localScale = t.localScale;
        showBall.transform.SetParent(oldParent);
    }

    IEnumerator Grab()
    {
        //rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(1);
    }

    void ThrowBall()
    {
        if (!_inventoryUI.UseCurrent())
            return;
        showBall.enabled = false;
        //GameObject newDodgeball = GameObject.Instantiate(dodgeball);
        Vector2 pos = (Vector2)transform.position + (aimingDir * 1.5f);
        GameObject newProj = Instantiate(projectileTypes[catchedBall]?.template, pos, Quaternion.identity);
        Projectile proj = newProj.GetComponent<Projectile>();
        proj.direction = aimingDir;
        newProj.layer = LayerMask.NameToLayer("ProjectileFromAlly");
        proj.speed = 30;
        proj.thrownByPlayer = true;
        /*
        newProj.transform.position = transform.position + (Vector3)(aimingDir * 1.5f);
        Debug.Log(aimingDir);
        newProj.GetComponent<Rigidbody2D>().velocity = aimingDir * 50;
        Debug.Log(newProj.GetComponent<Rigidbody2D>().velocity);
        */

    }

    void OnMove(InputValue input)
    {
        Vector2 lastMovement = movementInput;
        movementInput = input.Get<Vector2>();
        //Debug.Log(input);
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

        rigid.velocity = Vector3.MoveTowards(rigid.velocity, targetVel, Acceleration);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        spriteAnim.SetBool("Dashing", true);
        rigid.velocity = movementInput * (Speed * DashMultiplier);
        GetComponent<PlayerHealth>().BeInvincibleForTime(DashTime);
        yield return new WaitForSeconds(DashTime);
        dashing = false;
        spriteAnim.SetBool("Dashing", false);
        yield return new WaitForSeconds(DashRecoveryTime);
        canDash = true;
    }



    private void Update()
    {
        showBallParent.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, aimingDir) * Mathf.Sign(Vector3.Cross(Vector3.up, aimingDir).z));
    }
}
