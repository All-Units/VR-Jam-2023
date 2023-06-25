using UnityEngine;
using UnityEngine.Serialization;

public class GazeFollower : MonoBehaviour
{
    public float radius;
    public Transform player;

    private void Update()
    {
        var cameraTransform = player.transform;
        transform.LookAt(cameraTransform, Vector3.up);

        var targetPos = cameraTransform.position + cameraTransform.forward * radius;

        transform.position = targetPos;
    }
}
