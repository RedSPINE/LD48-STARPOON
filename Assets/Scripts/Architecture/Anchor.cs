using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public AudioEvent gripEvent;
    private ParticleSystem particles;
    private bool hasPlayer = false;
    public bool HasPlayer
    {
        get => hasPlayer;
        set {
            hasPlayer = value;
            EvaluateParticle();
        }
    }
    public GameObject GripSFXPrefab;

    private void Awake() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (HasPlayer) return;
        if(other.CompareTag("Harpoon"))
        {
            Instantiate(GripSFXPrefab, transform.position, Quaternion.identity);
            gripEvent.Play(GetComponent<AudioSource>());
            HasPlayer = true;
        }
    }

    private void EvaluateParticle()
    {
        if (!hasPlayer)
            particles.Play();
        else
            particles.Stop();
    }

}
