using UnityEngine;

public class GrapplingRope : MonoBehaviour {
    private Spring spring;
    private LineRenderer lr;
    private Vector3 currentGrapplePosition;
    public GrapplingGun grapplingGun;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeight;
    public AnimationCurve affectCurve;
    [SerializeField] private float maxLerpTime = 12f;
    private float currentLerpTime = 0;
    private bool reachedMaxDistance;
    

    void Awake() {
        lr = GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);
    }
    
    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    void DrawRope() 
    {
        var grapplePoint = grapplingGun.GetGrapplePoint();
        var gunTipPosition = grapplingGun.gunTip.position;
        var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

        //If not grappling, don't draw rope
        if (!grapplingGun.IsGrappling())
        {
            currentLerpTime -= Time.deltaTime * 1.3f;
            currentLerpTime = Mathf.Clamp(currentLerpTime, 0, maxLerpTime);

            if(currentLerpTime <= 0)
            {
                currentGrapplePosition = grapplingGun.gunTip.position;
                spring.Reset();
                if (lr.positionCount > 0)
                    lr.positionCount = 0;
                return;
            }
        }
        else
        {
            reachedMaxDistance = currentLerpTime + Time.deltaTime >= maxLerpTime;
            
            currentLerpTime += Time.deltaTime;
            currentLerpTime = Mathf.Clamp(currentLerpTime, 0, maxLerpTime);
        }

        if (lr.positionCount == 0) {
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }
        
        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);
        
        currentGrapplePosition = Vector3.Lerp(gunTipPosition, grapplePoint, currentLerpTime / maxLerpTime);

        if (!grapplingGun.IsGrappling() && reachedMaxDistance && grapplingGun.grappledItem)
        {
            grapplingGun.grappledItem.transform.position = currentGrapplePosition;
        }
        
        DrawPoints(up, gunTipPosition);
    }

    private void DrawPoints(Vector3 up, Vector3 gunTipPosition)
    {
        for (var i = 0; i < quality + 1; i++)
        {
            var delta = i / (float)quality;
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                         affectCurve.Evaluate(delta);

            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }
    }
}