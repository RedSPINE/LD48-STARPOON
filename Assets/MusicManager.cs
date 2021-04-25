using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Start() {
        SoundSettings.Instance.SwitchEvent.AddListener(OnSoundSwitch);
    }

    private void OnSoundSwitch()
    {
        var settings = SoundSettings.Instance;
        GetComponent<AudioSource>().mute = settings.Mode == SoundSettings.SoundMode.Off;
    }
}
