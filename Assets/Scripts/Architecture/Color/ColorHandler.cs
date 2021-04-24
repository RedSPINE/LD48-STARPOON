using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviour, IColorHandler
{
    [Serializable]
    public class SpriteGroup
    {
        public int tag;
        public List<SpriteRenderer> sprites;
    }

    public List<SpriteGroup> spriteGroups;

    [Serializable]
    public class ImageGroup
    {
        public int tag;
        public List<Image> images;
    }

    public List<ImageGroup> imageGroups;

    [Serializable]
    public class RawImageGroup
    {
        public int tag;
        public List<RawImage> rawImages;
    }

    public List<RawImageGroup> RawImageGroups;

    private void OnDestroy() {
        FindObjectOfType<ColorManager>().Unsubscribe(this);
    }

    private void Start() {
        FindObjectOfType<ColorManager>().Subscribe(this);
    }

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
            foreach (SpriteGroup spriteGroup in spriteGroups)
            {
                Color oldColor = oldPalette.colors[spriteGroup.tag];
                Color newColor = palette.colors[spriteGroup.tag];
                foreach (SpriteRenderer spriteRenderer in spriteGroup.sprites)
                {
                    spriteRenderer.color = Color.Lerp(oldColor, newColor, time / duration);
                }
            }
            foreach (ImageGroup imageGroup in imageGroups)
            {
                Color oldColor = oldPalette.colors[imageGroup.tag];
                Color newColor = palette.colors[imageGroup.tag];
                foreach (Image image in imageGroup.images)
                {
                    image.color = Color.Lerp(oldColor, newColor, time / duration);
                }
            }
            foreach (RawImageGroup rawImageGroup in RawImageGroups)
            {
                Color oldColor = oldPalette.colors[rawImageGroup.tag];
                Color newColor = palette.colors[rawImageGroup.tag];
                foreach (RawImage rawImage in rawImageGroup.rawImages)
                {
                    rawImage.color = Color.Lerp(oldColor, newColor, time / duration);
                }
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
