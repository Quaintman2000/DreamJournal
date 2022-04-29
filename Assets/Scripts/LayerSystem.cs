using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerSystem : MonoBehaviour
{
    public static LayerSystem Instance;

    [SerializeField]
    Texture2D copyFromSprite;
    [SerializeField] 
    FreeDraw.Drawable canvasPrefab;
    [SerializeField]
    List<FreeDraw.Drawable> canvases;
    [SerializeField]
    List<LayerButton> layerButtons;

    [SerializeField]
    LayerButton layerUIObjectPrefab;

    [SerializeField]
    ScrollRect panelContent;
    [SerializeField]
    Material defualtSpriteMaterial;

    int currentLayerIndex = 0;

    ArtCanvas canvas;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    private void Start()
    {
        
        if(ImageSaveAndLoad.LoadOldCanvas == true)
        {
           canvas = ImageSaveAndLoad.LoadArtCanvas(ImageSaveAndLoad.fileToLoad);
        }
        else
        {
        CreateNewLayer();

            List<Texture2D> texture2Ds = new List<Texture2D>();
            foreach(FreeDraw.Drawable image in canvases)
            {
                texture2Ds.Add(image.renderer.sprite.texture);
            }
            List<Material> materials = new List<Material>();
            foreach (FreeDraw.Drawable image in canvases)
            {
                materials.Add(image.renderer.material);
            }

            canvas = new ArtCanvas(texture2Ds, layerButtons, materials);
        }
    }

    [ContextMenu("New Layer")]
    public void CreateNewLayer()
    {
        Texture2D newTexture = new Texture2D(copyFromSprite.width, copyFromSprite.height);
        newTexture.SetPixels(copyFromSprite.GetPixels());
       
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, new Rect(0,0,newTexture.width, newTexture.height), new Vector2((0.5f),(0.5f)));

        Vector3 layerPosition = Vector3.back * 0.1f * canvases.Count;
        FreeDraw.Drawable newCanvas = Instantiate<FreeDraw.Drawable>(canvasPrefab, layerPosition, Quaternion.identity);
        newCanvas.renderer.sprite = newSprite;

        canvases.Add(newCanvas);
        currentLayerIndex = canvases.Count - 1;
        ChangeLayer(currentLayerIndex);

        // Spawn in the button.
        LayerButton newLayerButton = Instantiate<LayerButton>(layerUIObjectPrefab, panelContent.content);
        newLayerButton.LayerIndex = currentLayerIndex;
        newLayerButton.PreviewImage.sprite = newSprite;

        layerButtons.Add(newLayerButton);
    }

    public void ChangeLayer(int index)
    {
        currentLayerIndex = index;
        for (int i = 0; i < canvases.Count; i++)
        {
            if (i != index)
            {
                canvases[i].boxCollider.enabled = false;
                canvases[i].enabled = false;
            }
            else
            {
                canvases[i].boxCollider.enabled = true;
                canvases[i].enabled = true;
            }
        }
    }
    public void ChangeLayer(int index, LayerButton button)
    {
        button.buttonImage.color = Color.green;
        currentLayerIndex = index;
        for (int i = 0; i < canvases.Count; i++)
        {
            if (i != index)
            {
                canvases[i].boxCollider.enabled = false;
                canvases[i].enabled = false;
            }
            else
            {
                canvases[i].boxCollider.enabled = true;
                canvases[i].enabled = true;
                layerButtons[i].buttonImage.color = Color.white;
            }
        }
    }
    public void DeleteLayer()
    {
        Destroy(canvases[currentLayerIndex].gameObject);
        Destroy(layerButtons[currentLayerIndex].gameObject);
        canvases.RemoveAt(currentLayerIndex);
        layerButtons.RemoveAt(currentLayerIndex);

        if (currentLayerIndex > 0)
            ChangeLayer(currentLayerIndex - 1);
    }

    public void AdjustLayerOpacity(float value, int layerIndex)
    {
        canvases[currentLayerIndex].renderer.color = new Color(255, 255, 255, value);
    }

    public void AnimateLayer(Material animationMaterial)
    {
        if(animationMaterial != null)
        {
            canvases[currentLayerIndex].renderer.material = animationMaterial;
        }
        else
        {
            canvases[currentLayerIndex].renderer.material = defualtSpriteMaterial;
        }
    }
}

[System.Serializable]
public class ArtCanvas
{
    public List<Texture2D> canvases;
    public List<LayerButton> layerButtons;
    public List<Material> materials;

    public ArtCanvas (List<Texture2D> textures, List<LayerButton> layers, List<Material> mats)
    {
        canvases = textures;
        layerButtons = layers;
        materials = mats;
    }
}