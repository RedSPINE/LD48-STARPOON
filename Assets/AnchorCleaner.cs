using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorCleaner : MonoBehaviour
{
    private void Awake() {
        Anchor[] anchors = FindObjectsOfType<Anchor>();
        foreach (Anchor anchor in anchors)
        {
            if (anchor == null) continue;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(anchor.transform.position, 0.05f, Vector2.up);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == null) return;
                if (hit.collider.gameObject == anchor.gameObject) return;
                if (hit.collider.CompareTag("Anchor"))
                    Destroy(hit.collider.gameObject);
            }
        }
    }
}
