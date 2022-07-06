using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ColorPicker : MonoBehaviour
{
    // The preview image.
    public Image colorPreviewImage;
    // The rectransform
    RectTransform rect;
    // The color picker texture.
    Texture2D colorTexture;
    // The current color.
    public Color color;
    // The current opacity.
    [Range(0, 1)]
    public float opacity;
    // The slider UI to set our opacity.
    [SerializeField] Slider opacitySlider;
    // The text UI to display current opacity value.
    [SerializeField] TextMeshProUGUI opacityValueText;

    Coroutine colorSelectionCoroutine;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // When the color selection panel is enabled.
    private void OnEnable()
    {
        // Gets the recttransform.
        rect = GetComponent<RectTransform>();
        // Grabs the texture.
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
        // If the color selection method was already running, stop it.
        if (colorSelectionCoroutine != null)
            StopCoroutine(colorSelectionCoroutine);

        // Starts the color selection methods
        colorSelectionCoroutine = StartCoroutine(ColorSelection());
    }
    // When the color selection panel is disabled.
    private void OnDisable()
    {
        // If it's still running, stop it.
        if (colorSelectionCoroutine != null)
            StopCoroutine(colorSelectionCoroutine);
    }
    /// <summary>
    /// The function to pick out a color.
    /// </summary>
    /// <returns></returns>
    IEnumerator ColorSelection()
    {
        while (true)
        {
            // Get our mouse position over the color picker rectangle.
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out delta);
            // Get the width and height of the color picker rectangle.
            float width = rect.rect.width;
            float height = rect.rect.height;
            delta += new Vector2(width * .5f, height * .5f);
            // Clamp the X cordinate and Y cordinate between 0 and 1.
            float x = Mathf.Clamp(delta.x / width, 0, 1);
            float y = Mathf.Clamp(delta.y / height, 0, 1);
            // The pixel cordinate.
            int texX = Mathf.RoundToInt(x * colorTexture.width);
            int texY = Mathf.RoundToInt(y * colorTexture.height);
            // If we click within the color picker rectangle...
            if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            {
                // Get the color of that pixel at that position.
                color = colorTexture.GetPixel(texX, texY);
                // Set the color to use.
                colorPreviewImage.color = color;
                colorPreviewImage.color = new Color(colorPreviewImage.color.r, colorPreviewImage.color.g, colorPreviewImage.color.b, opacity);
            }
            yield return null;
        }
    }

    /// <summary>
    /// Sets our pen color.
    /// </summary>
    public void SetColor()
    {
        FreeDraw.Drawable.Pen_Colour = color;

    }
    /// <summary>
    /// Sets the layer's opacity.
    /// </summary>
    public void SetOpacity()
    {
        // Clamp the opacity value to be between 0 and 255.
        opacity = Mathf.Clamp(opacitySlider.value, 0, 255);
        opacityValueText.text = opacity.ToString();
        // Divide the value by 255 to get it between 0 and 1.
        opacity /= 255f;
        // Set the opacity of the preview image.
        colorPreviewImage.color = new Color(colorPreviewImage.color.r, colorPreviewImage.color.g, colorPreviewImage.color.b, opacity);
    }
}
