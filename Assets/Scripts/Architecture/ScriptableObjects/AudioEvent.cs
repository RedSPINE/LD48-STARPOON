using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event")]
public class AudioEvent : ScriptableObject
{
    [Min(0.1f)]
    public float interval;
    [Range(1, 10)]
    public int count;

    public AudioClip[] clips;
    [Range(0, 2)]
    public float minVolume = 1;
    [Range(0, 2)]
    public float maxVolume = 1;
    [Range(0, 2)]
    public float minPitch = 1;
    [Range(0, 2)]
    public float maxPitch = 1;

    public void Play(AudioSource source)
    {
        if (clips.Length == 0) return;
        if (count <= 1) PlayClip(source);
        else
        {
            source.GetComponent<MonoBehaviour>().StartCoroutine(FireClips(source));
        }
    }

    IEnumerator FireClips(AudioSource source)
    {
        float time = 0;
        int counter = 0;
        while (counter != count)
        {
            time += Time.deltaTime;
            if (time > interval)
            {
                PlayClip(source);
                counter++;
                time = 0;
            }
            yield return null;
        }
    }

    private void PlayClip(AudioSource source)
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = Random.Range(minVolume, maxVolume);
        if (SoundSettings.Instance.Mode == SoundSettings.SoundMode.Off) source.volume = 0;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }
}
