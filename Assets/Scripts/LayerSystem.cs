using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerSystem : MonoBehaviour
{
    // The current instance of the layer system.
    public static LayerSystem Instance;

    // The transparent sprite to copy from when making a new layer.
    [SerializeField]
    Texture2D copyFromSprite;
    // The canvas prefab to spawn.
    [SerializeField]
    FreeDraw.Drawable canvasPrefab;
    // A list of the canvases in the scene.
    [SerializeField]
    List<FreeDraw.Drawable> canvases;
    // A list of the layerbuttons in the scene.
    [SerializeField]
    List<LayerButton> layerButtons;
    
    // Layer button UI prefab.
    [SerializeField]
    LayerButton layerUIObjectPrefab;

    // The panel to put our layer buttons.
    [SerializeField]
    ScrollRect panelContent;
    // The defualt sprite material so the canvas dosesnt animate.
    [SerializeField]
    Material defualtSpriteMaterial;
    
    // An array of materials so can load the right materials when loading a old canvas.
    [SerializeField]
    Material[] loadMaterials;
    
    // The current layer index. 
    int currentLayerIndex = 0;

    // The canvas data.
    ArtCanvas canvas;


    private void Awake()
    {
        // If the instance isn't null...
        if (Instance != null)
            // Destroy that instance.
            Destroy(Instance.gameObject);
        // Set the current instance to this.
        Instance = this;

        // If we're not loading an old file...
        if (ImageSaveAndLoad.LoadingFile == false)
        {
            // Create a new canvas.
            int num = Random.Range(0, 9999);

            canvas = new ArtCanvas(("Canvas" + num));
        }

    }

    private void Start()
    {
        // If we are loading a file...
        if (ImageSaveAndLoad.LoadingFile == true)
        {
            // Set the canvas to the one we're loading, create the layers, and set the materials.
            canvas = ImageSaveAndLoad.LoadArtCanvas(ImageSaveAndLoad.fileToLoad);
            LoadCanvasLayers(canvas);
            LoadCanvasMaterials(canvas);
        }
        else
        {
            // If not, create a new layer .
            CreateNewLayer();
        }
    }

    /// <summary>
    /// Creates a new layer in the scene.
    /// </summary>
    [ContextMenu("New Layer")]
    public void CreateNewLayer()
    {
        // Make a copy of the transparent texture.
        Texture2D newTexture = new Texture2D(copyFromSprite.width, copyFromSprite.height);
        newTexture.SetPixels(copyFromSprite.GetPixels());
        newTexture.Apply();

        // Set the canvas to have that texture.
        canvas.SetTexture(newTexture);
        // Create a new sprite with that texture.
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2((0.5f), (0.5f)));
        
        // Set the position so it looks like it's infront of the previous layer.
        Vector3 layerPosition = Vector3.back * 0.1f * canvases.Count;
        // Spawn in the new canvas and set it's render to with the new sprite.
        FreeDraw.Drawable newDrawable = Instantiate<FreeDraw.Drawable>(canvasPrefab, layerPosition, Quaternion.identity);
        newDrawable.renderer.sprite = newSprite;
        
        // Add that layer to the canvas.
        canvases.Add(newDrawable);
        
        // Add material's name to the canvas's list of material names.
        canvas.materialsNames.Add(newDrawable.renderer.material.name);
        // Set the current layer index to the newest one.
        currentLayerIndex = canvases.Count - 1;
        
        // Change the layer.
        ChangeLayer(currentLayerIndex);

        // Spawn in the button.
        LayerButton newLayerButton = Instantiate<LayerButton>(layerUIObjectPrefab, panelContent.content);
        newLayerButton.LayerIndex = currentLayerIndex;
        newLayerButton.PreviewImage.sprite = newSprite;
        // Add the layer button to the list.
        layerButtons.Add(newLayerButton);

    }
    /// <summary>
    /// Creates a new layer (for loading old files only.)
    /// </summary>
    /// <param name="texture2D">The texture from the canvas.</param>
    void CreateNewLayer(Texture2D texture2D)
    {
        // Make a new sprite with the texture.
        Sprite newSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2((0.5f), (0.5f)));
        
        // Set the position so it looks like it's infront of the previous layer.
        Vector3 layerPosition = Vector3.back * 0.1f * canvases.Count;
        // Spawn in the new canvas and set it's render to with the new sprite.
        FreeDraw.Drawable newDrawable = Instantiate<FreeDraw.Drawable>(canvasPrefab, layerPosition, Quaternion.identity);
        newDrawable.renderer.sprite = newSprite;

        // Add that layer to the canvas.
        canvases.Add(newDrawable);

        canvases.Add(newDrawable);
        // Set the current layer index to the newest one.
        currentLayerIndex = canvases.Count - 1;

        // Change the layer.
        ChangeLayer(currentLayerIndex);

        // Spawn in the button.
        LayerButton newLayerButton = Instantiate<LayerButton>(layerUIObjectPrefab, panelContent.content);
        newLayerButton.LayerIndex = currentLayerIndex;
        newLayerButton.PreviewImage.sprite = newSprite;

        layerButtons.Add(newLayerButton);
    }
    /// <summary>
    /// Creates all the layers from the artcanvas data.
    /// </summary>
    /// <param name="artCanvas">The art canvas that holds the layers we want.</param>
    private void LoadCanvasLayers(ArtCanvas artCanvas)
    {
        // For each byte array in the art canvas's canvases...
        foreach(byte[] bytes in artCanvas.canvases)
        {
            // Make a new texture from it.
            Texture2D newTexture = new Texture2D(copyFromSprite.width, copyFromSprite.height);
            newTexture.LoadImage(bytes);
            newTexture.Apply();
            // Create the layer with that texture.
            CreateNewLayer(newTexture);
        }

    }
    /// <summary>
    /// Loads and applies all the materials for each layer.
    /// </summary>
    /// <param name="artCanvas">The artcanvas that contains the list of material names.</param>
    private void LoadCanvasMaterials(ArtCanvas artCanvas)
    {
        // For each material name in the list...
        for(int i = 0; i < artCanvas.materialsNames.Count; i++ )
        {
            // If it's the wavey material...
            if (artCanvas.materialsNames[i].Contains( "Shader Graphs_Wave"))
            {
                // Apply the wavey material.
                canvases[i].renderer.material = loadMaterials[0];
            }
            // If it's the defualt material...
            else if(artCanvas.materialsNames[i].Contains("Sprites-Default"))
            {
                // Apply the defualt material.
                canvases[i].renderer.material = defualtSpriteMaterial;
            }
        }
    }
    /// <summary>
    /// Changes the layer so we are drawing on the right one.
    /// </summary>
    /// <param name="index">The layer index to change to.</param>
    public void ChangeLayer(int index)
    {
        // Set the current layer index to the index we're changing to.
        currentLayerIndex = index;
        // For each canvas our list of canvases...
        for (int i = 0; i < canvases.Count; i++)
        {
            // If it's not at our index...
            if (i != index)
            {
                // Disable it so we don't draw on it.
                canvases[i].boxCollider.enabled = false;
                canvases[i].enabled = false;
            }
            else
            {
                // Enable it so we can draw on it.
                canvases[i].boxCollider.enabled = true;
                canvases[i].enabled = true;
            }
        }
    }
    /// <summary>
    /// Changes the layer when a button calls for it.
    /// </summary>
    /// <param name="index">The index to change to.</param>
    /// <param name="button">The layerbutton that called it.</param>
    public void ChangeLayer(int index, LayerButton button)
    {
        // Set the button color to green to show we selected it.
        button.buttonImage.color = Color.green;
        // Set the current index to the one we're changing to/
        currentLayerIndex = index;
        // For each canvas our list of canvases...
        for (int i = 0; i < canvases.Count; i++)
        {
            // If it's not at our index...
            if (i != index)
            {
                // Disable it so we don't draw on it.
                canvases[i].boxCollider.enabled = false;
                canvases[i].enabled = false;
                // Set the layer button color to white to show it's deselected.
                layerButtons[i].buttonImage.color = Color.white;
            }
            else
            {
                // Enable it so we can draw on it.
                canvases[i].boxCollider.enabled = true;
                canvases[i].enabled = true;
            }
        }
    }
    /// <summary>
    /// Deletes the layer.
    /// </summary>
    public void DeleteLayer()
    {
        // Destroy the gameobject.
        Destroy(canvases[currentLayerIndex].gameObject);
        // Remove it from the lists.
        canvas.canvases.RemoveAt(currentLayerIndex);
        canvas.materialsNames.RemoveAt(currentLayerIndex);
        // Destroy the layerbutton.
        Destroy(layerButtons[currentLayerIndex].gameObject);
        // Remove the button from the lists.
        canvases.RemoveAt(currentLayerIndex);
        layerButtons.RemoveAt(currentLayerIndex);
        // Go back a layer if we're not at 0.
        if (currentLayerIndex > 0)
            ChangeLayer(currentLayerIndex - 1);
    }
    /// <summary>
    /// Adjusts the opacity of the layer.
    /// </summary>
    /// <param name="value">The opacity value.</param>
    /// <param name="layerIndex">The index of the layer to change.</param>
    public void AdjustLayerOpacity(float value, int layerIndex)
    {
        // Set the opacity to the value.
        canvases[layerIndex].renderer.color = new Color(255, 255, 255, value);
    }
    /// <summary>
    /// Changes the material of the layer at the current layer index.
    /// </summary>
    /// <param name="animationMaterial">The animated material to place.</param>
    public void AnimateLayer(Material animationMaterial)
    {
        // If it is not null...
        if (animationMaterial != null)
        {
            // Set the material to the animation material.
            canvases[currentLayerIndex].renderer.material = animationMaterial;
        }
        else
        {
            // Set it back to the defualt material.
            canvases[currentLayerIndex].renderer.material = defualtSpriteMaterial;
        }
    }
    /// <summary>
    /// Animates the layer at the specified index.
    /// </summary>
    /// <param name="animationMaterial">The animated material.</param>
    /// <param name="index">The layer index.</param>
    void AnimateLayer(Material animationMaterial, int index)
    {
        // If it is not null...
        if (animationMaterial != null)
        {
            // Set the material to the animation material.
            canvases[index].renderer.material = animationMaterial;
        }
        else
        {
            // Set it back to the defualt material.
            canvases[index].renderer.material = defualtSpriteMaterial;
        }
    }
    /// <summary>
    /// Updates and returns the current canvas.
    /// </summary>
    /// <returns></returns>
    public ArtCanvas GetCanvas()
    {
        // For each of the canvases...
        for (int i = 0; i < canvases.Count; i++)
        {
            // Encode the texture to a png.
            canvas.canvases[i] = canvases[i].renderer.sprite.texture.EncodeToPNG();
            // Get the names of all the materials.
            canvas.materialsNames[i] = canvases[i].renderer.material.name;
        }
        // Returns the canvas.
        return canvas;
    }
}

[System.Serializable]
public class ArtCanvas
{
    // Name of the canvas.
    public string name;
    // A list of byte arrays to store our textures.
    public List<byte[]> canvases = new List<byte[]>();
    // A list of strings to store the names of the materials used.
    public List<string> materialsNames = new List<string>();

    // Constructor.
    public ArtCanvas(string newName)
    {
        name = newName;
    }

    // Sets the textures to the current list.
    public void SetTextures(List<Texture2D> texture2Ds)
    {
        // Encode each texture used to a png.
        foreach (Texture2D texture in texture2Ds)
        {
            canvases.Add(texture.EncodeToPNG());
        }
    }
    // Sets a texture to the current list.
    public void SetTexture(Texture2D texture2D)
    {
        // Encode the texture to a png.
        canvases.Add(texture2D.EncodeToPNG());

    }
}