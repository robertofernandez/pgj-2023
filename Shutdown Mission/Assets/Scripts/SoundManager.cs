using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource batteryDestroy;
    public AudioClip[] batteryClips;
    public void PlaySound()
    {
        batteryDestroy.clip = batteryClips[Random.Range(0,batteryClips.Length)];
        batteryDestroy.Play();
    }

}
