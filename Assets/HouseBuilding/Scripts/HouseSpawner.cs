using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HouseSpawner : MonoBehaviour
{
    [Tooltip("The number of houses visible on each side of the sidewalk")]
    [SerializeField] private int housesPerSide = 20;
    [SerializeField] private float treePercentage = 0.5f;
    [SerializeField] private float packagePercentage = 0.5f;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> treePrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> packagePrefabs = new List<GameObject>();
    

    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private Transform roadParent;
    [SerializeField] private Transform ship;

    [SerializeField] private Transform availableParent;
    [SerializeField] private Transform activeParent;
    // Start is called before the first frame update
    void Awake()
    {
        FillPool();
        spawnHouses();
        spawnRoads();
    }

    private float nextHouseZ = 40f;
    private float nextRoadZ = 24f;
    // Update is called once per frame
    void Update()
    {
        float z = ship.position.z;
        if (z >= nextHouseZ)
        {
            nextHouseZ += 40f;
            lastZ += 40f;
            activateRow(lastZ);
        }

        if (z >= nextRoadZ)
        {
            nextRoadZ += 24f;
            lastRoadZ += 24f;
            Transform road = roadParent.GetChild(0);
            Vector3 pos = road.transform.position;
            pos.z = lastRoadZ;
            road.transform.position = pos;
            road.SetAsLastSibling();
        }
    }

    void FillPool()
    {
        //Each side of the sidewalk has n houses
        //It also has n houses behind it, mirrored on the other side of the street
        //4n houses visible at any given time, with n in reserve in the pool
        for (int i = 0; i < housesPerSide * 5; i++)
        {
            GameObject house = Instantiate(prefabs.GetRandom(), availableParent);
            house.transform.position = Vector3.zero;
            house.gameObject.SetActive(false);
            house.gameObject.name = $"House{i}";

            foreach (Transform child in house.transform.Find("PlantPoints"))
            {
                if (Random.Range(0f, 100f) > (treePercentage * 100f))
                    continue;
                GameObject tree = Instantiate(treePrefabs.GetRandom(), child);
                tree.transform.localPosition = Vector3.zero;
                tree.transform.localScale *= Random.Range(0.6f, 1.4f);

            }
        }
    }
    
    //The initial spawn wave
    private float lastZ;
    void spawnHouses()
    {
        int low = (housesPerSide / 2) * -1;
        for (int i = low; i < (housesPerSide / 2); i++)
        {
             float z = i * 40f;
             lastZ = z;
            /*
            _spawnHouseAt(40f, z);
            _spawnHouseAt(-40f, z, true);
            _spawnHouseAt(90f, z + 20, true);
            _spawnHouseAt(-90f, z + 20, false);
            */
            activateRow(z, false);
        }
    }

    void activateRow(float z, bool deactivate = true)
    {
        _activateHouseAt(40f, z);
        _activateHouseAt(-40f, z, true);
        _activateHouseAt(90f, z + 20, true);
        _activateHouseAt(-90f, z + 20);
        
        //Also deactivates last row
        if (deactivate)
        {
            for (int i = 0; i < 4; i++)
            {
                Transform house = activeParent.GetChild(0);
                house.gameObject.SetActive(false);
                house.parent = availableParent;
                foreach (Transform child in house.Find("PackagePoint"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
    
    /// <summary>
    /// Activates a house at the given X and Z
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="flipX"></param>
    void _activateHouseAt(float x, float z, bool flipX = false)
    {
        //Get a random child in the available pool
        int i = Random.Range(0, availableParent.childCount - 1);
        Transform house = availableParent.GetChild(i);
        
        //Move it to the active list
        house.parent = activeParent;
        //Set as last sibling
        //Lowest child index is furthest back
        house.SetAsLastSibling();
        house.localScale = Vector3.one;
        //Turn on and move
        house.gameObject.SetActive(true);
        house.position = new Vector3(x, 0f, z);
        if (flipX)
        {
            Vector3 scale = house.localScale;
            scale.x *= -1;
            house.localScale = scale;
        }

        StartCoroutine(_spawnPackages(house.Find("PackagePoint")));
    }

    IEnumerator _spawnPackages(Transform spawnPoint)
    {
        while (true)
        {
            if (Random.Range(0f, 100f) > (packagePercentage * 100f))
                yield break;
            GameObject package = Instantiate(packagePrefabs.GetRandom(), spawnPoint);
            Vector3 unit = Random.insideUnitSphere;
            unit.y = 0f;
            package.transform.localPosition = Vector3.zero;
            package.transform.localScale *= 1.4f;
            Vector3 rot = new Vector3(Random.Range(0f, 30f), Random.Range(0f, 360f), Random.Range(0f, 30f));
            package.transform.eulerAngles = rot;
            package.transform.Translate(unit);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private float lastRoadZ;
    void spawnRoads()
    {
        for (int i = (housesPerSide * -1); i < housesPerSide; i++)
        {
            GameObject roadSegment = Instantiate(roadPrefab, roadParent);
            float z = i * 24f;
            roadSegment.transform.Translate(new Vector3(0f, 0f, z));
            lastRoadZ = z;

        }
    }
    
    /*<summary>LEGACY</summary>
    
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
     */
}
