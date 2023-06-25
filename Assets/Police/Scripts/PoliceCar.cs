using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float lightTime = .5f;
    [SerializeField] private float tireRotateSpeed = 10f;

    [SerializeField] private float closeDistance = 10f;
    private Light[] lights;
    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
        lights[1].gameObject.SetActive(false);
        lights[3].gameObject.SetActive(false);
        StartCoroutine(alternateLights());
    }

    private static HashSet<PoliceCar> closeEnough = new HashSet<PoliceCar>();
    // Update is called once per frame
    void Update()
    {
        float z = (moveSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, z));
        bool close = Vector3.Distance(ShipMover.pos, transform.position) <= closeDistance;
        if (close)
        {
            moveSpeed = ShipMover.mover.moveSpeed;
            if (closeEnough.Contains(this) == false)
                closeEnough.Add(this);
        }

        if (!close && closeEnough.Contains(this))
            closeEnough.Remove(this);
        if (closeEnough.Count == 3)
            Quit();
            
            
    }

    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void OnDisable()
    {
        closeEnough.Remove(this);
    }

    IEnumerator alternateLights()
    {
        while (true)
        {
            foreach (Light light in lights)
                light.gameObject.SetActive(!light.gameObject.activeInHierarchy);
            yield return new WaitForSeconds(lightTime);
        }
    }

}
