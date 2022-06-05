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

    [SerializeField]
    Material[] loadMaterials;

    int currentLayerIndex = 0;

    ArtCanvas canvas;


    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;

        if (ImageSaveAndLoad.LoadOldCanvas == false)
        {
            List<Texture2D> texture2Ds = new List<Texture2D>();

            List<Material> materials = new List<Material>();

            int num = Random.Range(0, 9999);

            canvas = new ArtCanvas(("Canvas" + num));
        }

    }

    private void Start()
    {

        if (ImageSaveAndLoad.LoadOldCanvas == true)
        {
            canvas = ImageSaveAndLoad.LoadArtCanvas(ImageSaveAndLoad.fileToLoad);
            LoadCanvasLayers(canvas);
            LoadCanvasMaterials(canvas);
        }
        else
        {
            
            CreateNewLayer();


        }
    }

    [ContextMenu("New Layer")]
    public void CreateNewLayer()
    {
        Texture2D newTexture = new Texture2D(copyFromSprite.width, copyFromSprite.height);
        newTexture.SetPixels(copyFromSprite.GetPixels());

        newTexture.Apply();

        canvas.SetTexture(newTexture);

        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2((0.5f), (0.5f)));

        Vector3 layerPosition = Vector3.back * 0.1f * canvases.Count;
        FreeDraw.Drawable newDrawable = Instantiate<FreeDraw.Drawable>(canvasPrefab, layerPosition, Quaternion.identity);
        newDrawable.renderer.sprite = newSprite;

        canvases.Add(newDrawable);
        canvas.materialsNames.Add(newDrawable.renderer.material.name);
        currentLayerIndex = canvases.Count - 1;
        ChangeLayer(currentLayerIndex);

        // Spawn in the button.
        LayerButton newLayerButton = Instantiate<LayerButton>(layerUIObjectPrefab, panelContent.content);
        newLayerButton.LayerIndex = currentLayerIndex;
        newLayerButton.PreviewImage.sprite = newSprite;

        layerButtons.Add(newLayerButton);

    }

    void CreateNewLayer(Texture2D texture2D)
    {
        Sprite newSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2((0.5f), (0.5f)));

        Vector3 layerPosition = Vector3.back * 0.1f * canvases.Count;
        FreeDraw.Drawable newDrawable = Instantiate<FreeDraw.Drawable>(canvasPrefab, layerPosition, Quaternion.identity);
        newDrawable.renderer.sprite = newSprite;

        canvases.Add(newDrawable);
        currentLayerIndex = canvases.Count - 1;
        ChangeLayer(currentLayerIndex);

        // Spawn in the button.
        LayerButton newLayerButton = Instantiate<LayerButton>(layerUIObjectPrefab, panelContent.content);
        newLayerButton.LayerIndex = currentLayerIndex;
        newLayerButton.PreviewImage.sprite = newSprite;

        layerButtons.Add(newLayerButton);
    }

    private void LoadCanvasLayers(ArtCanvas artCanvas)
    {
        foreach(byte[] bytes in artCanvas.canvases)
        {
            Texture2D newTexture = new Texture2D(copyFromSprite.width, copyFromSprite.height);
            newTexture.LoadImage(bytes);
            newTexture.Apply();
            CreateNewLayer(newTexture);
        }

    }

    private void LoadCanvasMaterials(ArtCanvas artCanvas)
    {
        for(int i = 0; i < artCanvas.materialsNames.Count; i++ )
        {
            if (artCanvas.materialsNames[i].Contains( "Shader Graphs_Wave"))
            {
                canvases[i].renderer.material = loadMaterials[0];
            }
            else if(artCanvas.materialsNames[i].Contains("Sprites-Default"))
            {
                canvases[i].renderer.material = defualtSpriteMaterial;
            }
        }
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
        canvas.canvases.RemoveAt(currentLayerIndex);
        canvas.materialsNames.RemoveAt(currentLayerIndex);
        Destroy(layerButtons[currentLayerIndex].gameObject);
        canvases.RemoveAt(currentLayerIndex);
        layerButtons.RemoveAt(currentLayerIndex);

        if (currentLayerIndex > 0)
            ChangeLayer(currentLayerIndex - 1);
    }

    public void AdjustLayerOpacity(float value, int layerIndex)
    {
        canvases[layerIndex].renderer.color = new Color(255, 255, 255, value);
    }

    public void AnimateLayer(Material animationMaterial)
    {
        if (animationMaterial != null)
        {
            canvases[currentLayerIndex].renderer.material = animationMaterial;
        }
        else
        {
            canvases[currentLayerIndex].renderer.material = defualtSpriteMaterial;
        }
    }
    void AnimateLayer(Material animationMaterial, int index)
    {
        if (animationMaterial != null)
        {
            canvases[index].renderer.material = animationMaterial;
        }
        else
        {
            canvases[index].renderer.material = defualtSpriteMaterial;
        }
    }

    public ArtCanvas GetCanvas()
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            canvas.canvases[i] = canvases[i].renderer.sprite.texture.EncodeToPNG();
            canvas.materialsNames[i] = canvases[i].renderer.material.name;
        }
        return canvas;
    }
}

[System.Serializable]
public class ArtCanvas
{
    public string name;

    public List<byte[]> canvases = new List<byte[]>();
    public List<string> materialsNames = new List<string>();
    public ArtCanvas(string newName)
    {
        name = newName;
        
    }

    public void SetTextures(List<Texture2D> texture2Ds)
    {
        foreach (Texture2D texture in texture2Ds)
        {
            canvases.Add(texture.EncodeToPNG());
        }
    }
    public void SetTexture(Texture2D texture2D)
    {

        canvases.Add(texture2D.EncodeToPNG());

    }
}