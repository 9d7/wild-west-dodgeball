using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float MaxDistance;

    [SerializeField] private float FollowSpeed;

    [SerializeField] private Transform playerTransform;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Follow();
    }
    void Update()
    {
        /*
        if (Vector3.Distance(transform.position, playerTransform.position) > MaxDistance)
        {
            Vector2 targetPosition = Vector3.MoveTowards(playerTransform.position, transform.position, MaxDistance); 
            transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);
        }
        */
    }
    void Follow()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = targetPosition;
    }
}
