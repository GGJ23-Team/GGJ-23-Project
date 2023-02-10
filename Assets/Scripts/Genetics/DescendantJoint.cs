using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescendantJoint : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public float lineWidth = 0.1f;
    public float lineOffset = -50f;

    void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }

    public void SetLine(Vector2 start, Vector2 parentLeft, Vector2 parentRight )
    { 
        Vector3 endPosition = new Vector3(parentLeft.x + (parentRight.x - parentLeft.x) / 2, Mathf.Min(parentLeft.y, parentRight.y) + lineOffset, 0);
        float midPointY = start.y + (endPosition.y - start.y) / 2 ;

        lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0));
        lineRenderer.SetPosition(1, new Vector3(start.x, midPointY, 0));
        lineRenderer.SetPosition(2, new Vector3 (endPosition.x, midPointY, 0) );
        lineRenderer.SetPosition(3, endPosition);
    }
}
