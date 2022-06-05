using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ System.Serializable]
public class LayerButton : MonoBehaviour
{
    // Name of the layer.
    public string LayerName;
    // Input field to name the layer.
    [SerializeField] InputField layerNameInputField;
    // The index of the layer.
    public int LayerIndex = 0;
    // The preview image.
    public Image PreviewImage;
    // The button image.
    public Image buttonImage;
    // The opacity slider.
    [SerializeField] Slider opacitySlider;
    /// <summary>
    /// The function that is called when clicked.
    /// </summary>
    public void OnClick()
    {
        // Changes the layer to this one.
        LayerSystem.Instance.ChangeLayer(LayerIndex, this);
    }
    // Updates the layer's name when the input field's text value changes.
    public void UpdateLayerName()
    {
        LayerName = layerNameInputField.text;
    }
    /// <summary>
    /// Sets the opacity when the slider value changes.
    /// </summary>
    public void OnOpacitySliderValueChanged()
    {
        LayerSystem.Instance.AdjustLayerOpacity(opacitySlider.value, LayerIndex);
    }
}
