using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public AudioEvent gripEvent;
    private ParticleSystem particles;
    [SerializeField]
    private bool hasPlayer = false;
    public bool HasPlayer
    {
        get => hasPlayer;
        set
        {
            hasPlayer = value;
            EvaluateParticle();
        }
    }
    public GameObject GripSFXPrefab;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayer) return;
        if (other.CompareTag("Harpoon"))
        {
            Instantiate(GripSFXPrefab, transform.position, Quaternion.identity);
            gripEvent.Play(GetComponent<AudioSource>());
            HasPlayer = true;
        }
    }

    private void EvaluateParticle()
    {
        if (hasPlayer)
            particles.Stop();
        else
            particles.Play();
    }

}
