using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour, IExplode
{
    [SerializeField] private List<GameObject> explosionFX = new();
    [SerializeField] private float explosionScale = 100f;
    [SerializeField] private float yOffset = 5f;
    public GameObject targetBox;
    public GameObject myBox;
    
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
        //HoardManager.DeactivateBox(targetBox);
        myBox.SetActive(true);
    }

    public void EndRun()
    {
        Destroy(gameObject);
    }
}
