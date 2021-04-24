using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    public float counter;
    public AudioEvent audioEvent;

    private void Start() {
        audioEvent?.Play(GetComponent<AudioSource>());
    }

    private void Update() {
        counter -= Time.deltaTime;
        if (counter < 0)
            Destroy(this.gameObject);
    }

    
}
