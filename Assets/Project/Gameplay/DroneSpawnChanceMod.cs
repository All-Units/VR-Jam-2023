using UnityEngine;

[CreateAssetMenu(menuName = "SO/Difficulty Mod/DroneSpawnChanceMod", fileName = "New DroneSpawnChanceMod")]
public class DroneSpawnChanceMod : DifficultyModifier
{
    public float spawnChance;
    
    public override void Apply()
    {
        GameManager.instance.droneSpawnChance = spawnChance;
    }
}