using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PowerUp : MonoBehaviour, IColorHandler
{
    [SerializeField] private GameObject pent1;
    [SerializeField] private GameObject pent2;
    public GameObject CollectVFXPrefab; 

    private void Start()
    {
        Vector3 rot1 = new Vector3(0, 0, 360);
        Vector3 rot2 = new Vector3(0, 0, -360);
        pent1.transform.DORotate(rot1, 2, RotateMode.FastBeyond360).SetLoops(-1);
        pent2.transform.DORotate(rot2, 2, RotateMode.FastBeyond360).SetLoops(-1);
    }

    public void LoadPalette()
    {
        Color oldColor1 = pent1.GetComponentInChildren<SpriteRenderer>().color;
        Color oldColor2 = pent2.GetComponentInChildren<SpriteRenderer>().color;
        ColorManager manager = FindObjectOfType<ColorManager>();
        Color newColor1 = manager.palettes[manager.palette].colors[1];
        Color newColor2 = manager.palettes[manager.palette].colors[3];
        StartCoroutine(LerpColors(oldColor1, oldColor2, newColor1, newColor2, manager.timeToTransition));
    }

    IEnumerator LerpColors(Color oldColor1, Color oldColor2, Color newColor1, Color newColor2, float duration)
    {
        float time = 0;
        SpriteRenderer rend1 = pent1.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer rend2 = pent2.GetComponentInChildren<SpriteRenderer>();

        while (time < duration)
        {
            rend1.color = Color.Lerp(oldColor1, newColor1, time / duration);
            rend2.color = Color.Lerp(oldColor2, newColor2, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public float lengthBonus;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Harpoon"))
        {
            FindObjectOfType<PlayerLogic>().range += lengthBonus;
            Die();
        }
    }

    private void Die()
    {
        pent1.transform.DOKill();
        pent2.transform.DOKill();
        GameObject vfxGO = Instantiate(CollectVFXPrefab, transform.position, Quaternion.identity);
        vfxGO.transform.localScale = CollectVFXPrefab.transform.localScale;
        FindObjectOfType<ColorManager>().NexPalette();
        Destroy(this.gameObject);
    }

}
