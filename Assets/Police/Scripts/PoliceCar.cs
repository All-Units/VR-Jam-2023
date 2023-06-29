using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour, IExplode
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float lightTime = .5f;
    [SerializeField] private float tireRotateSpeed = 10f;

    [SerializeField] private float closeDistance = 10f;
    [SerializeField] private List<GameObject> explosionFX = new List<GameObject>();
    private Light[] lights;

    public static Action OnEndGame;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        lights[1].gameObject.SetActive(false);
        lights[3].gameObject.SetActive(false);
        StartCoroutine(alternateLights());
        OnEndGame += OnEndGameInternal;
    }

    private void OnDestroy()
    {
        OnEndGame -= OnEndGameInternal;
    }

    private void OnEndGameInternal()
    {
        gameOver = true;
    }

    private static HashSet<PoliceCar> closeEnough = new HashSet<PoliceCar>();
    // Update is called once per frame
    void Update()
    {
        float z = (moveSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, z));
        
        if(gameOver) return;
        
        bool close = Vector3.Distance(ShipMover.pos, transform.position) <= closeDistance;
        if (close)
        {
            moveSpeed = ShipMover.mover.moveSpeed;
            if (closeEnough.Contains(this) == false)
                closeEnough.Add(this);
        }

        if (!close && closeEnough.Contains(this))
            closeEnough.Remove(this);
        if (closeEnough.Count == 3)
            EndGame();

    }

    private static void EndGame()
    {
        OnEndGame?.Invoke();
    }

    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnDisable()
    {
        closeEnough.Remove(this);
    }

    IEnumerator alternateLights()
    {
        while (true)
        {
            foreach (Light light in lights)
                light.gameObject.SetActive(!light.gameObject.activeInHierarchy);
            yield return new WaitForSeconds(lightTime);
        }
    }

    public float explosionScale = 3f;
    public float yOffset = 5f;
    public void Explode()
    {
        GameObject explosion = Instantiate(explosionFX.GetRandom());
        explosion.transform.position = transform.position + new Vector3(0f, yOffset, 3f);
        Destroy(explosion, 3f);
        explosion.transform.localScale *= explosionScale;
        Transform sound = transform.Find("AudioListener");
        if (sound != null)
        {
            sound.parent = null;
            sound.gameObject.SetActive(true);
        }
        
        //Destroy(sound.gameObject, 2f);
        
        //UnityEditor.EditorApplication.isPaused = true;
        Destroy(gameObject);
    }
}


