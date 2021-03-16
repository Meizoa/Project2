using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 5f;

    Rigidbody rb;
    Vector3 movement;

    float curCameraRotation = 0f;
    public Transform mainCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float yRotation = Input.GetAxis("Mouse X");
        float xRotation = Input.GetAxis("Mouse Y");

        Move(h, v);
        TurningY(yRotation);
        TurningX(xRotation);
    }
    void Move(float h, float v)
    {
        movement = (transform.right * h + transform.forward * v) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
    void TurningY(float yRotation)
    {
        Vector3 _yRotation = new Vector3(0f, yRotation, 0f) * mouseSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(_yRotation));
    }
    void TurningX(float xRotation)
    {
        float _xRotation = xRotation * mouseSensitivity;
        curCameraRotation -= _xRotation;
        curCameraRotation = Mathf.Clamp(curCameraRotation, -80, 80);

        mainCamera.localEulerAngles = new Vector3(curCameraRotation, 0f, 0f);//위아래
    }
}
