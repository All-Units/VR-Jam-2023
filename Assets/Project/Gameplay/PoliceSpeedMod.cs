using UnityEngine;

[CreateAssetMenu(menuName = "SO/Difficulty Mod/PoliceSpeedMod", fileName = "New PoliceSpeedMod")]
public class PoliceSpeedMod : DifficultyModifier
{
    public float newSpeed;
    
    public override void Apply()
    {
        GameManager.instance.policeSpeed = newSpeed;
    }
}