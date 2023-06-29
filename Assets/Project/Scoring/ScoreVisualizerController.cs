using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVisualizerController : MonoBehaviour
{
    private TMP_Text _tmpText;
    
    private void Awake()
    {
        HoardManager.onScoreChange += OnScoreChange;
    }

    private void OnDestroy()
    {
        HoardManager.onScoreChange -= OnScoreChange;
    }

    private void OnScoreChange(int obj)
    {
        _tmpText.text = obj.ToString();
    }

    private void OnValidate()
    {
        _tmpText = GetComponentInChildren<TMP_Text>();
    }
}
