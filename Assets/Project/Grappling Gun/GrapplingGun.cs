using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrapplingGun : MonoBehaviour {
    
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip;
    private float maxDistance = 100f;
    private bool isGrappling;

    public Grappleable grappledItem;
    public GameObject aimIcon;
    private bool _isSelected;

    private void Update()
    {
        if (_isSelected)
        {
            ShowAim();
        }
    }

    public void OnSelected()
    {
        _isSelected = true;
    }

    public void OnDeselected()
    {
        _isSelected = false;
        HideAimIcon();
    }

    private void ShowAim()
    {
        if (Physics.Raycast(gunTip.position, gunTip.forward, out var hit, maxDistance, whatIsGrappleable))
        {
            aimIcon.transform.position = hit.point;
            if(aimIcon.activeSelf == false)
                aimIcon.SetActive(true);

        }
        else
        {
            HideAimIcon();
        }
    }

    private void HideAimIcon()
    {
        if (aimIcon.activeSelf)
            aimIcon.SetActive(false);
    }

    private void HideAim()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    public void StartGrapple() 
    {
        RaycastHit hit;
        if (Physics.Raycast(gunTip.position, gunTip.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            isGrappling = true;
            
            grappledItem = hit.collider.gameObject.GetComponent<Grappleable>();
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