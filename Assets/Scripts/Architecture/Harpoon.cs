using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class Harpoon : MonoBehaviour
{
    private PlayerLogic player;
    private Transform playerT;
    private Vector2 target;
    public float speed = 1;
    private bool stuck;

    private void Awake() {
        player = FindObjectOfType<PlayerLogic>();
        playerT = player.transform;
        stuck = false;
    }

    private void Update() {

        // Good rotation
        transform.right = playerT.position - transform.position;
        // Come back to player if too far
        if ((Vector2.Distance(playerT.position, transform.position) >= player.range
        || Vector2.Distance(new Vector2(target.x, target.y), transform.position)< 0.1f) && !stuck)
        {
            SetTarget(playerT.position);
        }
        if(player.hasHarpoon)
            transform.position = playerT.position;
    }

    // Called by player when firing
    public void SetTarget(Vector2 targetPos)
    {
        transform.DOKill();
        target = targetPos;
        transform.DOMove(target, 1/speed);
    }

    // Collision trigger detection
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ennemy"))
        {
            SetTarget(playerT.position);
        }
        else if(other.CompareTag("Player"))
        {
            player.hasHarpoon = true;
            stuck = false;
            transform.DOKill();
            transform.position = other.transform.position;
            target = Vector2.zero;
        }
        else if(other.CompareTag("Anchor") && ( player.currentAnchor == null || other.gameObject != player.currentAnchor.gameObject))
        {
            player.SnapToAnchor(other.transform);
            transform.DOKill();
            transform.position = other.transform.position;
            stuck = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.CompareTag("Player") && !DOTween.IsTweening(transform) && !player.hasHarpoon)
        {
            player.hasHarpoon = true;
        }
    }
}
