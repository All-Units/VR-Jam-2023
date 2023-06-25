using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PoliceCar car = other.GetComponentInParent<PoliceCar>();
        if (car != null)
        {
            Destroy(car.gameObject);    
        }
        
        //Destroy(gameObject);
    }
}
