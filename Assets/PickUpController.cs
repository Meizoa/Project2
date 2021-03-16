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
        //transform.position = Vector3.Lerp(transform.position, boxContainer.position, Time.deltaTime * 100);
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

        //rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //rb.AddForce(mainCamera.forward * dropForwardForce, ForceMode.Impulse);
        //rb.AddForce(mainCamera.up * dropUpwardForce, ForceMode.Impulse);

        //float random  = Random.Range(-1f, 1f);
        //rb.AddTorque(new Vector3(random, random, random) * 10);

        //특정상황에 대한 스크립트 오프 예를 들면 던지는스크립트
    }
}
