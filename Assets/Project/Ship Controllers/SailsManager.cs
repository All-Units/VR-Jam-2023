using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SailsManager : MonoBehaviour
{
    public List<SailController> sailControllers;
    
    [Range(0,1)]
    public float currentValue;

    private void Update()
    {
        foreach (SailController sailController in sailControllers)
        {
            sailController.UpdateSail(currentValue);
        }
    }
    
}
