using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class HoardManager : MonoBehaviour
{
    private static HoardManager _instance;
    public int _score = 0;
    public static Action<int> onScoreChange;
    
    [SerializeField] private List<GameObject> availableHoard;
    [SerializeField] private List<GameObject> activeHoard;

    private void Awake()
    {
        _instance = this;
        PlayerPrefs.SetInt("currentScore", 0);
    }

    private void AddToHoard()
    {
        _score++;
        GameManager.instance.ModifyScore(1);

        onScoreChange?.Invoke(_score);
        
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

    public GameObject GetActiveBox()
    {
        return activeHoard.GetRandom();
    }

    public static void DeactivateBox(GameObject box)
    {
        if(_instance)
            _instance._DeactivateBox(box);
    }

    private void _DeactivateBox(GameObject box)
    {
        if (activeHoard.Contains(box))
        {
            activeHoard.Remove(box);
            availableHoard.Add(box);
            box.SetActive(false);
            
            _score--;
            GameManager.instance.ModifyScore(-2);
            PlayerPrefs.SetInt("currentScore", _score);
            if (_score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", _score);
            }
            onScoreChange?.Invoke(_score);
        }
    }

    public bool HasBoxes()
    {
        return activeHoard.Any();
    }
}