using System;
using UnityEngine;

public class TwoPointLineRenderer : MonoBehaviour
{
    [SerializeField] private Transform point1, point2;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(point1 == null || point2 == null) return;
        
        var points = new Vector3[2];
        points[0] = point1.position;
        points[1] = point2.position;

        _lineRenderer.SetPositions(points);
    }
}
