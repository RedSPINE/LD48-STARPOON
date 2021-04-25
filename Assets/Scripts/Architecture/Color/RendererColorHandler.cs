using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererColorHandler : MonoBehaviour, IColorHandler
{
    [Serializable]
    public class MaterialGroup
    {
        public int tag;
        public List<Material> materials;
    }

    public List<MaterialGroup> materialGroups;

    public void LoadPalette()
    {
        ColorManager manager = FindObjectOfType<ColorManager>();
        ColorPaletteSO palette = manager.palettes[manager.palette];
        ColorPaletteSO oldPalette = manager.palettes[manager.oldPalette];
        StartCoroutine(LerpColors(oldPalette, palette, manager.timeToTransition));
    }

    IEnumerator LerpColors(ColorPaletteSO oldPalette, ColorPaletteSO palette, float duration)
    {
        float time = 0;
        while(time < duration)
        {
            foreach (MaterialGroup materialGroup in materialGroups)
            {
                Color oldColor = oldPalette.colors[materialGroup.tag];
                Color newColor = palette.colors[materialGroup.tag];
                foreach (Material material in materialGroup.materials)
                {
                    material.SetColor("_TintColor", Color.Lerp(oldColor, newColor, time / duration));
                    material.SetColor("_Color", Color.Lerp(oldColor, newColor, time / duration));
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
