using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }

    private Dictionary<Sound, AudioClip> soundAudioClipDictonary = new Dictionary<Sound, AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        int i = 0;
        foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            Debug.Log(i);
            soundAudioClipDictonary[sound] = Resources.Load<AudioClip>(sound.ToString());
            ++i;
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictonary[sound]);
    }
}
