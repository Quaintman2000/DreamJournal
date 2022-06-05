using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ System.Serializable]
public class LayerButton : MonoBehaviour
{
    public string LayerName;
    [SerializeField] InputField layerNameInputField;
    public int LayerIndex = 0;
    public Image PreviewImage;
    public Image buttonImage;
    [SerializeField] Slider opacitySlider;

    public void OnClick()
    {
        LayerSystem.Instance.ChangeLayer(LayerIndex, this);
    }

    public void UpdateLayerName()
    {
        LayerName = layerNameInputField.text;
    }

    public void OnOpacitySliderValueChanged()
    {
        LayerSystem.Instance.AdjustLayerOpacity(opacitySlider.value, LayerIndex);
    }
}
