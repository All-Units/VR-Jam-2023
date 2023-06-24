using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();

    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Transform roadParent;
    // Start is called before the first frame update
    void Start()
    {
        spawnHouses();
        spawnRoads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnHouses()
    {
        for (int i = 0; i < 15; i++)
        {
            //GameObject house = Instantiate(prefabs.GetRandom(), transform);
            float z = i * 40f - 240f;
            _spawnHouseAt(40f, z);
            _spawnHouseAt(-40f, z, true);
            _spawnHouseAt(90f, z + 20, true);
            _spawnHouseAt(-90f, z + 20, false);
            //house.transform.position = new Vector3(40f, 0f, z);
            
            //house = Instantiate(prefabs.GetRandom(), transform);
            //house.transform.position = new Vector3(-40f, 0f, z);
            //Vector3 scale = house.transform.localScale;
            //scale.x *= -1;
            //house.transform.localScale = scale;
        }
    }
    
    /// <summary>
    /// Spawns a house at the given X and Z
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="flipX"></param>
    void _spawnHouseAt(float x, float z, bool flipX = false)
    {
        GameObject house = Instantiate(prefabs.GetRandom(), transform);
        house.transform.position = new Vector3(x, 0f, z);
        if (flipX)
        {
            Vector3 scale = house.transform.localScale;
            scale.x *= -1;
            house.transform.localScale = scale;
        }
    }

    void spawnRoads()
    {
        for (int i = -15; i < 15; i++)
        {
            GameObject roadSegment = Instantiate(roadPrefab, roadParent);
            roadSegment.transform.Translate(new Vector3(0f, 0f, i * 24f));
        }
    }
}
