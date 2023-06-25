using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI scoreText;
    private int b;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshPro scoreText2;
    // Start is called before the first frame update
    void Start()
    {
        scoreText2.text = $"Last score: {PlayerPrefs.GetInt("currentScore")}\n" +
                         $"High score: {PlayerPrefs.GetInt("highScore")}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu(){
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Restart()
    {
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }
    
}
