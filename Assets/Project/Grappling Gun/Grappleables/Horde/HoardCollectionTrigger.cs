using UnityEngine;

public class HoardCollectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collecting! {other.gameObject.name}");
        
        if (!other.TryGetComponent(out Grappleable grappleable)) return;
        
        var pos = grappleable.transform.position;
        // Destroy Grabbed object
        Destroy(grappleable.gameObject);

        // Spawn particles at prev location
            
        // Turn on hoard object
        HoardManager.Collect();
    }
}
