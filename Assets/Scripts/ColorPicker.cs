using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ColorPicker : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public Image colorPreviewImage;
    RectTransform rect;
    Texture2D colorTexture;
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out delta);

        string debug = "mousePostion = " + Input.mousePosition;


        float width = rect.rect.width;
        float height = rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);
        debug += "<br>delta = " + delta;

        float x = Mathf.Clamp(delta.x / width, 0, 1);
        float y = Mathf.Clamp(delta.y / height, 0, 1);

        int texX = Mathf.RoundToInt(x * colorTexture.width);
        int texY = Mathf.RoundToInt(y * colorTexture.height);

        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(rect,Input.mousePosition))
        {
            color = colorTexture.GetPixel(texX, texY);

            colorPreviewImage.color = color;
        }
    }
}
