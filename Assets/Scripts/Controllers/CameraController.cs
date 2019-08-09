using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float minZoom = 5f, maxZoom = 15f;
    [SerializeField] private float currentZoom = 10f;
    [SerializeField] private float pitch = 2f;
    [SerializeField] private float yawSpeed = 100f;

    private float currentYaw = 0f;

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        transform.position = Target.position - offset * currentZoom;
        transform.LookAt(Target.position + Vector3.up * pitch);

        transform.RotateAround(Target.position, Vector3.up, currentYaw);
    }
}
