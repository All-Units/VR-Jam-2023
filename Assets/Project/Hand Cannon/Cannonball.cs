using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Colliding with {other.gameObject.name}");
        if (other.TryGetComponent(out IExplode explodable))
        {
            explodable.Explode();
            Destroy(gameObject);
            return;
        }
        
        var car = other.GetComponentInParent<PoliceCar>();
        if (car != null)
        {
            car.Explode();
            Destroy(gameObject);
            return;
        }
    }
}

public interface IExplode
{
    public void Explode();
}
