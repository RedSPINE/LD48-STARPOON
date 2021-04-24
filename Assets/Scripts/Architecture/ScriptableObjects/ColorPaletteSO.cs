using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObjects/ColorPalette")]
public class ColorPaletteSO : ScriptableObject
{
    public static int colorCount;
    public List<Color> colors;
}
