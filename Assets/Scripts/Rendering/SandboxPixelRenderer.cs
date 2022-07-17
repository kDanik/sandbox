using UnityEngine;
using System.Collections.Generic;

public class SandboxPixelRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject renderObject;

    private Texture2D texture;
    
    // copy of Texture2D used to apply changes to pixels once every iteration instead of using SetPixel for each change
    private Color32[] texturePixelArray;

    // texture size in pixels
    // (texture.height and texture.width is MUCH slower)
    private int width;
    private int height;

    /// <summary>
    /// Creates texture with given width and height and create renderObject that this texture is attached to.
    /// Scales renderObject depending on given width, height and pixel size
    /// </summary>
    public void InitRenderObject(int width, int height, float pixelSizeInUnityUnits)
    {
        this.height = height;
        this.width = width;

        texturePixelArray = new Color32[height * width];
        texture = new Texture2D(width, height)
        {
            filterMode = FilterMode.Point // filter mode for pixeled textures
        };


        // create renderObject, scale it depending on pixel size and width, height and set it's texture
        renderObject.transform.localScale = new Vector3(width * pixelSizeInUnityUnits, height * pixelSizeInUnityUnits, 1);
        renderObject.transform.position = new Vector3(width * pixelSizeInUnityUnits / 2 - pixelSizeInUnityUnits / 2, height * pixelSizeInUnityUnits / 2 - pixelSizeInUnityUnits / 2, 1);
        renderObject.GetComponent<Renderer>().material.mainTexture = texture;

        // set all pixels to transparent color
        ClearAllPixels();
    }


    /// <summary>
    /// Sets all pixels in the texture to transparent Color32(0,0,0,0)
    /// </summary>
    public void ClearAllPixels()
    {
        var textureRawData = texture.GetRawTextureData<Color32>();

        for (int i = 0; i < textureRawData.Length; i++) textureRawData[i] = new Color32(0, 0, 0, 0);

        texture.Apply();
    }


    /// <returns>
    /// returns color of the pixel with given position
    /// if out of bounds, return Color32(0,0,0,0)
    /// </returns>
    public Color32 GetPixel(int x, int y)
    {
        if (!IsPositionInBounds(x, y)) return new Color32(0, 0, 0, 0);

        return texture.GetPixel(x, y);
    }

    /// <summary>
    /// Sets color of pixel with given position, if position is in bounds of texture
    /// </summary>
    public void SetPixel(int x, int y, Color32 color)
    {
        if (!IsPositionInBounds(x, y)) return;

        texturePixelArray[y * width + x] = color;
    }

    /// <summary>
    /// Applies changes of pixels (texture.SetPixel) to texture, if hasChangesSinceLastApply is true
    /// </summary>
    public void ApplyCurrentChangesToTexture()
    {
        texture.SetPixels32(texturePixelArray);

        texture.Apply();
    }

    /// <returns>
    /// true if position is in bounds of texture, otherwise false
    /// </returns>
    private bool IsPositionInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }   
}
