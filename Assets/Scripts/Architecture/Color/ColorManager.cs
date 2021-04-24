using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorManager : MonoBehaviour
{
    public List<IColorHandler> colorHandlers;

    public int palette = 0;
    public int oldPalette = 0;
    [Range(0, 5)]
    public float timeToTransition = 1;
    public List<ColorPaletteSO> palettes;

    public void Unsubscribe(IColorHandler handler)
    {
        if (colorHandlers.Contains(handler))
            colorHandlers.Remove(handler);
    }

    public void Subscribe(IColorHandler handler)
    {
        if (colorHandlers == null) Debug.LogError("WTFFFF");
        if (!colorHandlers.Contains(handler))
            colorHandlers.Add(handler);
    }

    private void Awake()
    {
        colorHandlers = new List<IColorHandler>();
    }

    private void Start()
    {

        LoadPalette();
    }

    public void LoadPalette()
    {
        var handlers = Find<ColorHandler>();
        foreach (IColorHandler colorHandler in handlers)
        {
            colorHandler.LoadPalette();
        }

        // foreach (IColorHandler handler in colorHandlers)
        // {
        //     handler.LoadPalette();
        // }
    }

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
}
