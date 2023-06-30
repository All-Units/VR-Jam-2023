using UnityEngine;

[CreateAssetMenu(menuName = "SO/Difficulty Mod/PoliceSpawnRateMod", fileName = "New PoliceSpawnRateMod")]
public class PoliceSpawnRateMod : DifficultyModifier
{
    public float newRate;
    
    public override void Apply()
    {
        GameManager.instance.policeSpawnRate = newRate;
    }
}