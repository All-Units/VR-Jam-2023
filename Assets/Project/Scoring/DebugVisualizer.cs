using TMPro;
using UnityEngine;

public class DebugVisualizer : MonoBehaviour
{
    private TMP_Text _tmpText;

    private void Update()
    {
        var text = $"Drone Check Rate: {GameManager.instance.droneSpawnCheckRate}s\n" +
                   $"Drone Spawn Chance: {GameManager.instance.droneSpawnChance} | {GameManager.instance.GetDroneRolls()}\n" +
                   $"Police Spawn Rate: {GameManager.instance.policeSpawnRate}\n" +
                   $"Police Speed: {GameManager.instance.policeSpawnRate}s" +
                   $"Level: {GameManager.instance.GetLevel()}";

        _tmpText.text = text;
    }

    private void OnValidate()
    {
        _tmpText = GetComponentInChildren<TMP_Text>();
    }
}