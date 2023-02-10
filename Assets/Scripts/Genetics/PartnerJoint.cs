using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerJoint : MonoBehaviour
{
     private LineRenderer lineRenderer;

    public float lineWidth = 0.1f;
    public float lineOffset = -50f;

    void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }

    public void SetLine(Vector2 start, Vector2 end)
    {
        Vector3 newStart = new Vector3(start.x, start.y, 0);
        Vector3 newEnd = new Vector3(end.x, end.y, 0);

        lineRenderer.SetPosition(0, newStart);
        lineRenderer.SetPosition(1, newStart + new Vector3(0, lineOffset, 0));
        lineRenderer.SetPosition(2, newEnd + new Vector3(0, lineOffset, 0));
        lineRenderer.SetPosition(3, newEnd);
    }
}
