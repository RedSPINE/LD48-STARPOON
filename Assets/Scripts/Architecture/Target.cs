using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Vector3 nativeScale;

    private void Awake() {
        nativeScale = transform.localScale;
    }

    public void PunchScale(float intensity)
    {
        transform.localScale = nativeScale;
        transform.DOPunchScale(Vector3.one * intensity, 0.2f);
        transform.DORestart();
    }
}
