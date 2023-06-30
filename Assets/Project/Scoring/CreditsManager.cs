using TMPro;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText2;
    // Start is called before the first frame update
    private void Start()
    {
        scoreText2.text = $"Last score: {PlayerPrefs.GetInt(ScoreTracker.BoxesCollectedCurrent)}\n" +
                         $"High score: {PlayerPrefs.GetInt(ScoreTracker.BoxesCollectedHighscore)}\n";
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
