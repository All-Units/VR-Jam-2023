using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

[ExecuteInEditMode]
public class SailController : MonoBehaviour
{
    public float maxPosValue, minPosValue;

    public void UpdateSail(float val)
    {
        val = Mathf.Clamp(val, 0.15f, 1);
        
        transform.localScale = new Vector3(1,val, 1);

        transform.position = new Vector3(transform.position.x, Mathf.Lerp(maxPosValue, minPosValue, val),
            transform.position.z);
    }
}
