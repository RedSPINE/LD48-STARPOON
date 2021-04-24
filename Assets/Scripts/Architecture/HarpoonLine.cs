using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class HarpoonLine : MonoBehaviour
{
    Transform player;
    Transform harpoon;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = FindObjectOfType<PlayerLogic>().transform;
        harpoon = FindObjectOfType<Harpoon>().transform;
    }

    private void Update()
    {
        var playerPos = player.position;
        var harpoonPos = harpoon.position; 
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, playerPos);
        lineRenderer.SetPosition(1, harpoonPos);
    }
}
