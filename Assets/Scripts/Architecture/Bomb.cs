using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public EnnemyData data;
    public Vector3 direction;
    public Transform gluedTransform;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // direction = (FindObjectOfType<PlayerLogic>().transform.position - transform.position).normalized;
        transform.DOShakeScale(3f, 0.1f, 10, 90, false).SetLoops(-1);
        GetComponent<ColorHandler>().LoadPalette();
    }

    private void Update() {
        if (gluedTransform == null)
            transform.position = transform.position + direction * data.speed * Time.deltaTime;
        else
            transform.position = gluedTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLogic>().Damage();
            Die();
        }
        else if (other.CompareTag("Harpoon"))
        {
            gluedTransform = other.transform;
        }
        else if (other.CompareTag("Ennemy") || other.CompareTag("Bomb"))
        {
            Detonate();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if (other.collider.CompareTag("Ennemy"))
        {
            Detonate();
        }
    }

    private float detonateRadius = 0.75f;
    public bool isDetonating = false;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detonateRadius);
    }
    
    public void Detonate()
    {
        if (isDetonating) return;
        isDetonating = true;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detonateRadius, Vector2.up);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;
            if (hit.collider.CompareTag("Bomb"))
            {
                hit.collider.GetComponent<Bomb>().Detonate();
            }
            else if (hit.collider.CompareTag("Ennemy"))
            {
                hit.collider.GetComponentInParent<Ennemy>().Die();
            }
        }
        Die();
    }

    private void Die()
    {
        transform.DOKill();
        GameObject explosionGO = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionGO.transform.localScale = explosionPrefab.transform.localScale;
        Destroy(this.gameObject);
    }
}
