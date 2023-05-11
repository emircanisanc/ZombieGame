using UnityEngine;

namespace AudioEvents
{
[CreateAssetMenu(menuName ="AudioEvent/New Simple Audio Event")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] audioClips;
    public RangedFloat volume;
    public RangedFloat pitch;

    public override void Play(AudioSource source)
    {
        if(audioClips.Length == 0)
            return;
        source.clip = audioClips[Random.Range(0, audioClips.Length-1)];
        source.volume = volume.RandomValue;
        source.pitch = pitch.RandomValue;
        source.Play();
    }
}
}