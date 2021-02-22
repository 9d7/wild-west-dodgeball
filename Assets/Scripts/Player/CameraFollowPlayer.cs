using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float MaxDistance;

    [SerializeField] private float FollowSpeed;

    [SerializeField] private Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > MaxDistance)
        {
            Vector2 targetPosition = Vector3.MoveTowards(playerTransform.position, transform.position, MaxDistance); 
            transform.position = Vector3.Lerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);
        }
    }
}
