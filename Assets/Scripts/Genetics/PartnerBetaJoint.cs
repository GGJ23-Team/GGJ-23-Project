using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBetaJoint : MonoBehaviour
{
     private LineRenderer lineRenderer;

    public float lineWidth = 0.1f;
    public float lineOffsetY = -50f;
    public float nodeSpacingX = 150f;
    private bool isPartnerLeft = false;
    [Range (0f, 50f)]
    private float clearanceX = 10f;
    [Range (0f, 50f)]
    private float clearanceY = 10f;

    void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
    }

    public void SetLine(Vector2 start, Vector2 end)
    {

        // Add clearance
        if (lineOffsetY < 0) {
            clearanceY = -clearanceY;
        }
        if (nodeSpacingX < 0) {
            clearanceX = -clearanceX;
        }

        // Start is alwais lowest
        if (Mathf.Min( start.y, end.y ) == end.y) {
            Vector2 temp = start;
            start = end;
            end = temp;
        }

        // Set lineOffsetY symbol
        if (Mathf.Min(start.x, end.x) == start.x && end.x != end.y) nodeSpacingX = -nodeSpacingX; 

        Vector3 newStart = new Vector3(start.x, start.y, 0);
        Vector3 newEnd = new Vector3(end.x, end.y, 0);

        lineRenderer.SetPosition(0, newStart );
        lineRenderer.SetPosition(1, new Vector3( newStart.x, newStart.y +lineOffsetY, 0) );
        lineRenderer.SetPosition(2, new Vector3( newStart.x -nodeSpacingX/2 +clearanceX, newStart.y +lineOffsetY, 0) );
        lineRenderer.SetPosition(3, new Vector3( newStart.x -nodeSpacingX/2 +clearanceX, newEnd.y +lineOffsetY +clearanceY, 0) );
        lineRenderer.SetPosition(4, new Vector3( newEnd.x, newEnd.y +lineOffsetY +clearanceY, 0) );
        lineRenderer.SetPosition(5, newEnd );
    }
}
