using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Image image;
    public Sprite On;
    public Sprite Off;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        SoundSettings.Instance.SwitchEvent.AddListener(UpdateButton);
        UpdateButton();
    }

    private void UpdateButton()
    {
        Debug.Log("UpdateMuteButton");
        if (SoundSettings.Instance.Mode == SoundSettings.SoundMode.On)
            image.sprite = On;
        else
            image.sprite = Off;
    }
}
