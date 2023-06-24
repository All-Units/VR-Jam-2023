using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoardManager : MonoBehaviour
{
    private static HoardManager _instance; 
    
    [SerializeField] private List<GameObject> availableHoard;
    [SerializeField] private List<GameObject> activeHoard;

    private void Awake()
    {
        _instance = this;
    }

    public void AddToHoard()
    {
        var obj = availableHoard.GetRandom();
        availableHoard.Remove(obj);
        
        activeHoard.Add(obj);
        obj.SetActive(true);
    }

    public static void Collect()
    {
        if(_instance)
            _instance.AddToHoard();
    }
}
