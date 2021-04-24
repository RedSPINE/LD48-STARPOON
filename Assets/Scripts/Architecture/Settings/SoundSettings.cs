using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Settings/SoundSettings")]
public class SoundSettings : SingletonScriptableObject<SoundSettings>
{
    public enum SoundMode
    {
        On,
        Off
    }
    [SerializeField]
    private SoundMode mode;
    public SoundMode Mode { get => mode; }

    public void Switch()
    {
        if (mode == SoundMode.On)
            mode = SoundMode.Off;
        else
            mode = SoundMode.On;
        SwitchEvent.Invoke();
    }

    public UnityEvent SwitchEvent;
}
