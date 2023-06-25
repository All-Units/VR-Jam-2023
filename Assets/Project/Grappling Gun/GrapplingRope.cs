using UnityEngine;
using UnityEngine.Events;

public class GrapplingRope : MonoBehaviour {
    private Spring _spring;
    private LineRenderer _lineRenderer;
    private Vector3 _currentGrapplePosition;
    public GrapplingGun grapplingGun;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHeight;
    public AnimationCurve affectCurve;
    public AnimationCurve retractCurve;
    [SerializeField] private float maxLerpTime = 12f;
    private float _currentLerpTime = 0;
    private bool _reachedMaxDistance;
    private Rigidbody _gunRigidbody;
    public GameObject hook;
    public Vector3 hookHomePosition;

    public ShipMover ship;

    public UnityEvent OnHit;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
        _spring = new Spring();
        _spring.SetTarget(0);
        _gunRigidbody = grapplingGun.GetComponent<Rigidbody>();
        hookHomePosition = hook.transform.localPosition;
    }
    
    //Called after Update
    private void Update() {
        DrawRope();
    }

    private void DrawRope() 
    {
        var grapplePoint = grapplingGun.GetGrapplePoint();
        var gunTipPosition = grapplingGun.gunTip.position + (Vector3.forward * (ship.moveSpeed * Time.deltaTime));
        
        var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

        //If not grappling, don't draw rope
        if (!grapplingGun.IsGrappling())
        {
            _currentLerpTime -= Time.deltaTime * retractCurve.Evaluate(_currentLerpTime / maxLerpTime);
            _currentLerpTime = Mathf.Clamp(_currentLerpTime, 0, maxLerpTime);

            if(_currentLerpTime <= 0)
            {
                _currentGrapplePosition = grapplingGun.gunTip.position;
                _spring.Reset();
                if (_lineRenderer.positionCount > 0)
                    _lineRenderer.positionCount = 0;
                hook.transform.localPosition = hookHomePosition;
                return;
            }
        }
        else
        {
            var prevReached = _reachedMaxDistance;
            _reachedMaxDistance = _currentLerpTime + Time.deltaTime >= maxLerpTime;

            if (prevReached != _reachedMaxDistance && _reachedMaxDistance)
            {
                if(grapplingGun.grappledItem)
                    OnHit?.Invoke();
            }
            
            _currentLerpTime += Time.deltaTime;
            _currentLerpTime = Mathf.Clamp(_currentLerpTime, 0, maxLerpTime);
        }

        if (_lineRenderer.positionCount == 0) {
            _spring.SetVelocity(velocity);
            _lineRenderer.positionCount = quality + 1;
        }
        
        _spring.SetDamper(damper);
        _spring.SetStrength(strength);
        _spring.Update(Time.deltaTime);
        
        _currentGrapplePosition = Vector3.Lerp(gunTipPosition, grapplePoint, _currentLerpTime / maxLerpTime);
        hook.transform.position = _currentGrapplePosition - (Vector3.forward * (ship.moveSpeed * Time.deltaTime));
        // hook.transform.rotation = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized);

        if (!grapplingGun.IsGrappling() && _reachedMaxDistance && grapplingGun.grappledItem)
        {
            grapplingGun.grappledItem.transform.position = _currentGrapplePosition;
        }
        
        DrawPoints(up, gunTipPosition);

        /*if (_reachedMaxDistance && grapplingGun.IsGrappling())
            grapplingGun.IsGrappling();*/
    }

    private void DrawPoints(Vector3 up, Vector3 gunTipPosition)
    {
        for (var i = 0; i < quality + 1; i++)
        {
            var delta = i / (float)quality;
            var offset = _reachedMaxDistance ? Vector3.zero : up * (waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * _spring.Value * affectCurve.Evaluate(delta));

            _lineRenderer.SetPosition(i, Vector3.Lerp(gunTipPosition, _currentGrapplePosition, delta) + offset);
        }
    }
}