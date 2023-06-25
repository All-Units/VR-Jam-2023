using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform ship;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float spawnBehindDistance = 80f;

    private List<Transform> spawnPoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            spawnPoints.Add(child);
        StartCoroutine(spawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            int i = 0;
            Transform next = spawnPoints.GetRandom();
            //If the spawn point has a child, try a different one
            while (next.childCount != 0)
            {
                i++;
                if (i >= 5)
                    break;
                next = spawnPoints.GetRandom();
            }

            if (next.childCount == 0)
            {
                GameObject car = Instantiate(carPrefab, next);
                float z = ship.position.z - spawnBehindDistance;
                car.transform.position = new Vector3(next.position.x, 0f, z);
            }
        }
    }
}
