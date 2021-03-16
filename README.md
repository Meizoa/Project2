# Project2
프로젝트 저장소2

##캐릭터 움직임
```c#
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

```

##물체 줍기
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider coll;
    public Transform player, boxContainer, mainCamera;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        if (!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(.5f,.5f,.5f);//스케일이 계속이상해짐

        Vector3 distanceToPlayer = player.position - transform.position;

        if (!equipped && distanceToPlayer.magnitude <= pickUpRange &&
            Input.GetKeyDown(KeyCode.Mouse0))
        {
            PickUp();
        }
        else if (equipped && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Drop();
        }

        if (equipped)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void PickUp()
    {
        //equipped = true;
        rb.isKinematic = true;
        coll.isTrigger = true;
        transform.position = boxContainer.position;
        if(transform.position == boxContainer.position)
        {
            transform.SetParent(boxContainer);
            equipped = true;
        }

        //특정상황에 대한 스크립트 온 예를 들면 던지는스크립트
    }

    void Drop()
    {
        equipped = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;
        
        //특정상황에 대한 스크립트 오프 예를 들면 던지는스크립트
    }
}

```
