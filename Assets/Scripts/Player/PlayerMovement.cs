using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Options")] [SerializeField] private float speed;
    // Start is called before the first frame update

    private Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue input)
    {
        // TODO (Thomas) make this good
        Vector2 vec = input.Get<Vector2>();
        rigid.velocity = vec * speed;
    }

}
