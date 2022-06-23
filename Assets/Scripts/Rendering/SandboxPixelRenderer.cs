using UnityEngine;

public class SandboxPixelRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject renderObject;

    private Texture2D texture;

    private int width;
    private int height;

    // stores information if any new pixel colors where changed since last texture apply
    private bool hasChangesSinceLastApply = false;

    public void InitRenderObject(int width, int height, float pixelSizeInUnityUnits)
    {
        // TODO Setup   

        this.height = height;
        this.width = width;

        texture = new Texture2D(width, height)
        {
            filterMode = FilterMode.Point
        };

        renderObject.transform.localScale = new Vector3(width * pixelSizeInUnityUnits, height * pixelSizeInUnityUnits, 1);
        renderObject.transform.position = new Vector3(width * pixelSizeInUnityUnits / 2 - pixelSizeInUnityUnits / 2, height * pixelSizeInUnityUnits / 2 - pixelSizeInUnityUnits / 2, 1);
        renderObject.GetComponent<Renderer>().material.mainTexture = texture;

        texture.Apply();
    }

    // Gets the color of the pixel of rendered texture from coordinates.
    // if out of bounds, return Color.clear (0,0,0,0)
    public Color GetPixel(int x, int y)
    {
        if (!IsPositionInBounds(x, y)) return Color.clear;

        return texture.GetPixel(x, y);
    }

    // Sets color of pixel, if coordinates are not out of bound and color is different from current pixel color
    public void SetPixel(int x, int y, Color color)
    {
        if (!IsPositionInBounds(x, y)) return;

        if (GetPixel(x, y).Equals(color)) return;

        texture.SetPixel(x, y, color);

        hasChangesSinceLastApply = true;
    }

    // Appplies changes to pixels (texture.SetPixel) if hasChangesSinceLastApply is true
    public void ApplyCurrentChangesToTexture()
    {
        // if no changes were made, textures doesnt need to be updated
        if (!hasChangesSinceLastApply) return;

        texture.Apply();

        hasChangesSinceLastApply = false;
    }


    private bool IsPositionInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

}
