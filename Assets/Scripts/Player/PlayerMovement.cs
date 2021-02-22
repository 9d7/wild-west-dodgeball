using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Options")] [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    // Start is called before the first frame update
    private Vector2 movementInput;
    

    private Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    // Runs on every physics timestep
    private void FixedUpdate()
    {
        Vector2 targetVel = movementInput * speed;

        rigid.velocity = Vector3.MoveTowards(rigid.velocity, targetVel, acceleration);
    }
}
