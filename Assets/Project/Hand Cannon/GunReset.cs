using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReset : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 startRot;

    private Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetPosition()
    {
        transform.localPosition = startPos;
        transform.localEulerAngles = startRot;
        rb.velocity = Vector3.zero;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "GunResetFloor")
        {
            ResetPosition();
        }
    }
}
