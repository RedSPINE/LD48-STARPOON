using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarUI : MonoBehaviour
{
    public bool filled = false;

    public void Fill()
    {
        filled = true;
        GetComponent<Image>().sprite = UISettings.Instance.fullStar;
        transform.DOShakeScale(0.1f);
        transform.DOShakeRotation(0.5f, 90, 10, 0);
    }
}
