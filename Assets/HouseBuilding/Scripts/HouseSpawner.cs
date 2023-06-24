using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnHouses();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnHouses()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject house = Instantiate(prefabs.GetRandom(), transform);
            float z = i * 40f - 240f;
            house.transform.position = new Vector3(50f, 0f, z);
            
            house = Instantiate(prefabs.GetRandom(), transform);
            house.transform.position = new Vector3(-50f, 0f, z);
            Vector3 scale = house.transform.localScale;
            scale.x *= -1;
            house.transform.localScale = scale;
        }
    }
}
