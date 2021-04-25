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
            other.GetComponent<PlayerLogic>().Die();
            Die();
        }
        else if (other.CompareTag("Harpoon"))
        {
            gluedTransform = other.transform;
        }
    }

    private void Die()
    {
        transform.DOKill();
        GameObject explosionGO = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionGO.transform.localScale = explosionPrefab.transform.localScale;
        Destroy(this.gameObject);
    }
}
