using Unity.Mathematics;
using UnityEngine;

public class HoardCollectionTrigger : MonoBehaviour
{
    public GameObject collectParticles;
    public AudioClipController audioClipController;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collecting! {other.gameObject.name}");
        
        if (!other.TryGetComponent(out Grappleable grappleable)) return;
        
        var pos = grappleable.transform.position;
        // Destroy Grabbed object
        Destroy(grappleable.gameObject);

        // Spawn particles at prev location
        var particles = Instantiate(collectParticles, pos, quaternion.identity);
        audioClipController.PlayClip();
        
        Destroy(particles, 2f);
            
        // Turn on hoard object
        HoardManager.Collect();
    }
}
