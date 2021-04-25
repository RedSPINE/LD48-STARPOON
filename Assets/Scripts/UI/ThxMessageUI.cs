using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThxMessageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakeRotation(3f, 10, 2, 30, false).SetLoops(-1);
    }
}
