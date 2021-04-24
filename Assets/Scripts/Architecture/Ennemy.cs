using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ennemy : MonoBehaviour
{
    public EnnemyData data;
    public Vector3 direction;
    private Transform player;
    [SerializeField]
    private GameObject DeathSFXPrefab;
    [SerializeField]
    private GameObject DetectSFXPrefab;
    [SerializeField]
    private Transform detectPoint;
    private bool onward;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLogic>().transform;
        transform.DOShakeScale(0.1f, 0.1f, 20, 90, false).SetLoops(-1);
        onward = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > data.detectionRange && onward == false) return;
        if (onward == false)
        {
            GameObject detectPrefab = Instantiate(DetectSFXPrefab, detectPoint.position, Quaternion.identity);
            detectPrefab.transform.localScale = DetectSFXPrefab.transform.localScale;
            onward = true;
        }
        direction = (player.position - transform.position).normalized;
        transform.up = direction;
        transform.position = transform.position + direction * Time.deltaTime * data.speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, data.detectionRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Head touched by " + other.tag);
        if (other.CompareTag("Harpoon"))
        {
            Die();
        }
    }

    private void Die()
    {
        transform.DOKill();
        GameObject deathSFX = Instantiate(DeathSFXPrefab, transform.position, Quaternion.identity);
        deathSFX.transform.localScale = DeathSFXPrefab.transform.localScale;
        Destroy(this.gameObject);
    }
}
