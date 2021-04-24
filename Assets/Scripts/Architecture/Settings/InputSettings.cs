using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings/InputSettings")]
public class InputSettings : SingletonScriptableObject<InputSettings>
{
    public enum ControlScheme
    {
        KeyboardMouse,
        Gamepad
    }
    private ControlScheme scheme;
    public ControlScheme Scheme { get => scheme; set => scheme = value; }
}
