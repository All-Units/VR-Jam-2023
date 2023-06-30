using UnityEngine;

[CreateAssetMenu(menuName = "SO/Difficulty Mod/DroneSpawnCheckRateMod", fileName = "New DroneSpawnCheckRateMod")]
public class DroneSpawnCheckRateMod : DifficultyModifier
{
    public float spawnCheckRate;
    
    public override void Apply()
    {
        GameManager.instance.droneSpawnCheckRate = spawnCheckRate;
    }
}