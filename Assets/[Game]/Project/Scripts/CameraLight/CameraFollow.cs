using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    private void Awake()
    {
        Instance = this;
    }

    //camera transform
    //public Transform camTransform;
    private Transform camTransform;
    public Transform CamTransform { get { return (camTransform == null) ? camTransform = GetComponent<Transform>() : camTransform; } }

    // camera will follow this object
    public Transform Target;
    // offset between camera and target
    public Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Offset = CamTransform.position - Target.position;
    }

    private void FixedUpdate()
    {
        // update position
        Vector3 targetPosition = Target.position + Offset;
        CamTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        // update rotation
        transform.LookAt(Target);
    }
}
