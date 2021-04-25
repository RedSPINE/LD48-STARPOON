using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorManager : MonoBehaviour
{
    public int palette = 0;
    public int oldPalette = 0;
    [Range(0, 5)]
    public float timeToTransition = 1;
    public List<ColorPaletteSO> palettes;

    private void Start()
    {
        LoadPalette();
    }

    public void LoadPalette()
    {
        var handlers = Find<IColorHandler>();
        foreach (IColorHandler colorHandler in handlers)
        {
            colorHandler.LoadPalette();
        }
    }

    // Found Code
    public static List<T> Find<T>()
    {
        List<T> interfaces = new List<T>();
        GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootGameObject in rootGameObjects)
        {
            T[] childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
            foreach (var childInterface in childrenInterfaces)
            {
                interfaces.Add(childInterface);
            }
        }
        return interfaces;
    }

    public void NexPalette()
    {
        if (oldPalette != palette) oldPalette = (oldPalette + 1) % palettes.Count;
        palette = (palette + 1) % palettes.Count;
        FindObjectOfType<StarCollectionUI>().FillNextStar();
        LoadPalette();
    }
}
