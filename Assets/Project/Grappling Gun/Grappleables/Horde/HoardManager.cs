using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoardManager : MonoBehaviour
{
    private static HoardManager _instance;
    private int _score = 0;
    public static Action<int> OnScoreChange;
    
    [SerializeField] private List<GameObject> availableHoard;
    [SerializeField] private List<GameObject> activeHoard;

    private void Awake()
    {
        _instance = this;
    }

    private void AddToHoard()
    {
        _score++;
        OnScoreChange?.Invoke(_score);
        
        if (availableHoard.Count == 0)
        {
            var go= activeHoard.GetRandom();
            go.SetActive(false);
            go.SetActive(true);
            return;
        }
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
