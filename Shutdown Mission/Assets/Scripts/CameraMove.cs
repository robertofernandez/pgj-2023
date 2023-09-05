using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float smothSpeed = 5f;
    public Vector3 offset;
    public float sideShake = 0.15f;
    public float zoomShake = 0.02f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        this.transform.position = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smothSpeed * Time.fixedDeltaTime);
        //transform.position = Vector3.MoveTowards(transform.position,desiredPosition,smothSpeed * Time.fixedDeltaTime);
         
    }

    public void CameraShake(Vector2 direction)
    {
        Vector3 direction3d = direction * sideShake;
        transform.position = transform.position + direction3d + offset * zoomShake;
    }
}