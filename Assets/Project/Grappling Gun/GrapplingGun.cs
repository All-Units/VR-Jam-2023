using UnityEngine;

public class GrapplingGun : MonoBehaviour {
    
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip;
    private float maxDistance = 100f;
    private bool isGrappling;

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    public void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            isGrappling = true;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        isGrappling = false;
    }



    public bool IsGrappling() {
        return isGrappling;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}