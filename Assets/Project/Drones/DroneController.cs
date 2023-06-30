using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DroneController : MonoBehaviour, IExplode
{
    [SerializeField] private List<GameObject> explosionFX = new();
    [SerializeField] private float explosionScale = 100f;
    [SerializeField] private float yOffset = 5f;
    [SerializeField] private Animator _animator;
    
    public GameObject targetBox;
    public GameObject myBox;
    private static readonly int InLr = Animator.StringToHash("In_LR");
    private static readonly int OutLr = Animator.StringToHash("Out_LR");
    private static readonly int InDir = Animator.StringToHash("In_Dir");
    private static readonly int OutDir = Animator.StringToHash("Out_Dir");

    public static Action OnDeath;

    private void Awake()
    {
        _animator.SetFloat(InLr, Random.Range(0, 2));
        _animator.SetFloat(OutLr, Random.Range(0, 2));
        _animator.SetFloat(InDir, Random.Range(0, 1f));
        _animator.SetFloat(OutDir, Random.Range(0, 1f));
    }

    private void OnDestroy()
    {
        GameManager.instance.ModifyScore(1);
        OnDeath?.Invoke();
    }

    public void Explode()
    {
        var explosion = Instantiate(explosionFX.GetRandom());
        explosion.transform.position = transform.position + new Vector3(0f, yOffset, 3f);
        Destroy(explosion, 3f);
        explosion.transform.localScale *= explosionScale;
        var sound = transform.Find("AudioListener");
        if (sound != null)
        {
            sound.parent = null;
            sound.gameObject.SetActive(true);
        }
        
        Destroy(gameObject);
    }

    public void Collect()
    {
        HoardManager.DeactivateBox(targetBox);
        myBox.SetActive(true);
    }

    public void EndRun()
    {
        Destroy(gameObject);
    }
}
