using System.Collections;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{

    public float fadeTime = 1f;

    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        PoliceCar.OnEndGame += OnEndGame;
    }

    private void OnDestroy()
    {
        PoliceCar.OnEndGame -= OnEndGame;
    }

    private void OnEndGame()
    {
        StartCoroutine(ShowEndScreen());
    }

    private IEnumerator ShowEndScreen()
    {
        Debug.Log("Fading in!");
        var t = 0f;
        while (t < fadeTime)
        {
            canvasGroup.alpha = Mathf.Clamp01(t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Waiting!");
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(3f);
        
        Debug.Log("Fading out!");
        t = fadeTime;
        while (t > 0)
        {
            canvasGroup.alpha = Mathf.Clamp01(t / fadeTime);
            t -= Time.deltaTime;
            yield return null;
        }
        
        Debug.Log("Complete!");
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
