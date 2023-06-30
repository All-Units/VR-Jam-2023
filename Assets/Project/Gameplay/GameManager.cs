using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float policeSpawnRate = 8;
    public float policeSpeed = 14;
    public float droneSpawnChance = 0;
    public float droneSpawnCheckRate = 10;

    public DifficultyScale scale;

    [SerializeField] private PoliceSpawner policeSpawner;
    [SerializeField] private DroneManager droneManager;
    [SerializeField] private HoardManager hoardManager;

    private int score = 0;
    public static Action<int> onScoreChange;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scale.currentLevel = 0;

        // StartCoroutine(Timer());
    }

    private bool pickedUpLauncher = false;
    public void OnPickUpLaunchers()
    {
        if(pickedUpLauncher) return;
        
        StartCoroutine(PoliceSpawnRoutine());
        StartCoroutine(DroneSpawnRoutine());
        pickedUpLauncher = true;
    }

    public void ModifyScore(int change)
    {
        score += change;
        scale.CheckScore(score);
        onScoreChange?.Invoke(score);
    }

    private IEnumerator PoliceSpawnRoutine()
    {
        var currentSpawnRate = policeSpawnRate;
        while (true)
        {
            currentSpawnRate -= Time.deltaTime;
            
            if (currentSpawnRate <= 0)
            {
                policeSpawner.Spawn(policeSpeed);
                currentSpawnRate = policeSpawnRate;
            }

            yield return null;
        }
    }

    private IEnumerator DroneSpawnRoutine()
    {
        var currentSpawnRate = droneSpawnCheckRate;
        while (true)
        {
            currentSpawnRate -= Time.deltaTime;
            
            if(hoardManager.HasBoxes() == false) yield return new WaitForSeconds(.5f);

            if (currentSpawnRate <= 0)
            {
                for (int i = 0; i < hoardManager._score/15; i++)
                {
                    if(Random.Range(0, 1f) <= droneSpawnChance)
                        droneManager.SpawnDrone();
                    
                    if(droneManager.totalSpawned < 3)
                        break;
                }

                currentSpawnRate = droneSpawnCheckRate;
            }

            yield return null;
        }
    }
    
    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            
            ModifyScore(1);
        }
    }

    public int GetLevel()
    {
        return scale.currentLevel;
    }

    public string GetDroneRolls()
    {
        return (hoardManager._score / 10).ToString();
    }
}