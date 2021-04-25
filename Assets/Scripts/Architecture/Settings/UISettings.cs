using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings/UISettings")]
public class UISettings : SingletonScriptableObject<UISettings>
{
    public Sprite emptyStar;
    public Sprite fullStar;
    
}
