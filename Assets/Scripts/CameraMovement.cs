using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;
    public float smoothSpeed = 0.125f;

    void Start()
    {
        // Calculate the initial offset, but only take the x and z positions into account
        offset = new Vector3(transform.position.x - playerTransform.position.x, transform.position.y, transform.position.z - playerTransform.position.z);
    }

    void LateUpdate()
    {
        // Desired position based on player's horizontal position and the offset
        Vector3 desiredPosition = new Vector3(playerTransform.position.x + offset.x, transform.position.y, playerTransform.position.z + offset.z);
        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}