using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererColorHandler : MonoBehaviour, IColorHandler
{
    public LineRenderer lineRenderer;
    public int colorTag;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void LoadPalette()
    {
        Debug.Log("Change Palette for LineRenderer");
        ColorManager manager = FindObjectOfType<ColorManager>();
        ColorPaletteSO palette = manager.palettes[manager.palette];
        ColorPaletteSO oldPalette = manager.palettes[manager.oldPalette];
        StartCoroutine(LerpColors(oldPalette, palette, manager.timeToTransition));
    }

    IEnumerator LerpColors(ColorPaletteSO oldPalette, ColorPaletteSO palette, float duration)
    {
        float time = 0;
        Color oldColor = oldPalette.colors[colorTag];
        Color newColor = palette.colors[colorTag];
        while (time < duration)
        {
            Color color = Color.Lerp(oldColor, newColor, time / duration);
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            time += Time.deltaTime;
            yield return null;
        }
    }
}
