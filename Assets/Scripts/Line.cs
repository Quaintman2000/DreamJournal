using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
    }

    bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0)
            return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1),pos) > DrawManager.RESOLUTION;
    }
}
