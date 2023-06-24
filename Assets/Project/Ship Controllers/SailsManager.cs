using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[ExecuteInEditMode]
public class SailsManager : MonoBehaviour
{
    public List<SailController> sailControllers;
    
    [Range(0,1)]
    public float currentValue;

    public static SailsManager instance;

    private void Awake()
    {
        instance = this;
    }

    [Editor]
    private void Update()
    {
        foreach (SailController sailController in sailControllers)
        {
            sailController.UpdateSail(currentValue);
        }
    }

    /// <summary>
    /// Controls all sails
    /// </summary>
    /// <param name="val">Value will be clamped to between 0-1</param>
    public void UpdateSailValue(float val)
    {
        foreach (var sailController in sailControllers)
        {
            sailController.UpdateSail(val);
        }
    }
}
