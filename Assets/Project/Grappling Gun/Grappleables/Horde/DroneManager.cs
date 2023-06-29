using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] private DroneController droneController;
    [SerializeField] private HoardManager hoardManager;

    [SerializeField] private float spawnRate = 2;
    private float _spawnCooldown = 0;

    private void Update()
    {
        if(hoardManager.HasBoxes() == false) return;
        
        _spawnCooldown -= Time.deltaTime;
        if (_spawnCooldown <= 0)
        {
            SpawnDrone();
            _spawnCooldown = spawnRate;
        }
    }

    public void SpawnDrone()
    {
        var box = hoardManager.GetActiveBox();

        var drone = Instantiate(droneController, box.transform);
        drone.targetBox = box;
    }
}