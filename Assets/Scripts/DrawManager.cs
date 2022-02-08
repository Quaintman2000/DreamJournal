using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour
{
    Camera cam;
    [SerializeField] Brush currentBrush;
    [SerializeField] Color brushColor;

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
        // If the pointer is not over a UI gameobject...
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // If the currentBrush is a continuous stroke type....
            if (currentBrush.brushType == Brush.BrushType.Continuous)
            {
                // If you click down...
                if (Input.GetMouseButtonDown(0))
                {
                    // Spawn the line.
                    currentLine = Instantiate(currentBrush.brushObject, mousePos, Quaternion.identity).GetComponent<Line>();

                    currentLine.GetComponent<LineRenderer>().material.color = brushColor;
                }
                // If you're holding it down...
                if (Input.GetMouseButton(0))
                {
                    // Add the next position to the line renderer.
                    currentLine.SetPosition(mousePos);
                }
                // If you release the click...
                if (Input.GetMouseButtonUp(0))
                {
                    // Simplify the line based on the brush stroke's tolerance.
                    currentLine.lineRenderer.Simplify(currentBrush.tolerance);
                }
            }
            // If the current brush is not continuous stroke type...
            else
            {
                // If you click down...
                if (Input.GetMouseButtonDown(0))
                {
                    // Spawn the brush.
                    GameObject brush = Instantiate(currentBrush.brushObject, mousePos, Quaternion.identity);
                    brush.GetComponent<LineRenderer>().material.color = brushColor;
                }
            }
        }
    }

    public void SetBrush(Brush brush)
    {
        currentBrush = brush;
    }

    public void SetColor(ColorPicker color)
    {
        brushColor = color.color;
    }
}
