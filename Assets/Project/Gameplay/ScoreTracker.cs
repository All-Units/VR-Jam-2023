using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static string BoxesCollectedCurrent = "boxes_currentScore";
    public static string BoxesCollectedHighscore = "boxes_highScore";
    
    public static string CopsBlownUpCurrent = "cops_currentScore";
    public static string CopsBlownUpHighscore = "cops_highScore";    
    
    public static string DronesBlownUpCurrent = "cops_currentScore";
    public static string DronesBlownUpHighscore = "cops_highScore"; 
    
    public static string timeCurrent = "time_currentScore";
    public static string timeHighscore = "time_highScore";

    private int _boxesScore = 0;
    private int _policeScore = 0;
    private int _droneScore = 0;
    private float _elapsedTime = 0.0f;

    private void Awake()
    {
        HoardManager.onScoreChange += OnBoxScoreChange;
        PoliceCar.OnDeath += OnPoliceDeath;
        DroneController.OnDeath += OnDroneDeath;
    }
    
    private void OnDestroy()
    {
        HoardManager.onScoreChange -= OnBoxScoreChange;
        PoliceCar.OnDeath -= OnPoliceDeath;
        DroneController.OnDeath -= OnDroneDeath;
        
        PlayerPrefs.SetInt(BoxesCollectedCurrent, _boxesScore);
        if (_boxesScore > PlayerPrefs.GetInt(BoxesCollectedHighscore))
        {
            PlayerPrefs.SetInt(BoxesCollectedHighscore, _boxesScore);
        }        
        
        PlayerPrefs.SetInt(CopsBlownUpCurrent, _policeScore);
        if (_policeScore > PlayerPrefs.GetInt(CopsBlownUpHighscore))
        {
            PlayerPrefs.SetInt(CopsBlownUpHighscore, _policeScore);
        }        
        
        PlayerPrefs.SetInt(DronesBlownUpCurrent, _droneScore);
        if (_droneScore > PlayerPrefs.GetInt(DronesBlownUpHighscore))
        {
            PlayerPrefs.SetInt(DronesBlownUpHighscore, _droneScore);
        }        
        
        PlayerPrefs.SetFloat(timeCurrent, _elapsedTime);
        if (_elapsedTime > PlayerPrefs.GetFloat(timeHighscore))
        {
            PlayerPrefs.SetFloat(timeHighscore, _elapsedTime);
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void OnDroneDeath()
    {
        _droneScore++;
    }

    private void OnPoliceDeath()
    {
        _policeScore++;
    }

    private void OnBoxScoreChange(int obj)
    {
        _boxesScore = obj;
    }
}