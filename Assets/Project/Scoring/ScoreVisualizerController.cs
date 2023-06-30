using TMPro;
using UnityEngine;

public class ScoreVisualizerController : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmpText;
    
    private void Awake()
    {
        HoardManager.onScoreChange += OnScoreChange;
        if(_tmpText == null)
            _tmpText = GetComponentInChildren<TMP_Text>();
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