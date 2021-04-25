using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarUI : MonoBehaviour
{
    public bool filled = false;
    public float filledScale = 1.2f;
    public Transform ball;

    private void Start() {
        ball.GetComponent<Image>().enabled = false;
    }

    public void Fill()
    {
        filled = true;
        transform.DOScale(Vector3.one * filledScale, 1);
        transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360);
        ball.GetComponent<Image>().enabled = true;
        ball.DOPunchScale(Vector3.one * 0.15f, 1, 10, 1).SetLoops(-1);
    }
}
