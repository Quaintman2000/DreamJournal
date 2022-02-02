using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    Camera cam;
    [SerializeField] Line linePrefab;
    [SerializeField] List<Line> lines;
    [SerializeField] float tolerance;

    public const float RESOLUTION = 0.1f;

    Line currentLine;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
        }
        if(Input.GetMouseButton(0))
        {
            currentLine.SetPosition(mousePos);
        }
        if(Input.GetMouseButtonUp(0))
        {
            currentLine.lineRenderer.Simplify(tolerance);
            lines.Add(currentLine);
        }
    }

  
    
}
