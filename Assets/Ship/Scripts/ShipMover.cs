using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour
{
    public float moveSpeed = 5f;

    public static ShipMover mover;
    // Start is called before the first frame update
    void Start()
    {
        mover = this;
    }

    public static Vector3 pos;
    // Update is called once per frame
    void Update()
    {
        float z = (moveSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, z));
        pos = transform.position;
    }

    public void ResetGunTo(Transform target, Transform gun)
    {
        gun.position = target.position;
    }
}
