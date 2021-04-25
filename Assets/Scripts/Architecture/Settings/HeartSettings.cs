using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings/HeartSettings")]
public class HeartSettings : SingletonScriptableObject<HeartSettings>
{
    [Min(0)]
    public float timeToScale;
    [Range(0, 1)]
    public float downScaleFactor;
    [Range(0, 10)]
    public int vibrato;
}
