using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Heart : MonoBehaviour
{
    public bool up;
    private Vector3 upScale;

    private void Awake() {
        up = true;
        upScale = transform.localScale;
    }

    public void Down()
    {
        transform.DOKill();
        var settings = HeartSettings.Instance;
        transform.localScale = upScale * settings.downScaleFactor;
        transform.DOPunchScale(upScale, settings.timeToScale, settings.vibrato);
        up = false;
    }

    public void Up()
    {
        transform.DOKill();
        var settings = HeartSettings.Instance;
        transform.localScale = upScale;
        transform.DOPunchScale(upScale * settings.downScaleFactor, settings.timeToScale, settings.vibrato);
        up = true;
    }
}
