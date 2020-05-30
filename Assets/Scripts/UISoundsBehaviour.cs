using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundsBehaviour : MonoBehaviour
{
    public List<AudioClip> interface_sounds;
    public AudioSource audio_source;

    public void playButtonHover()
    {
        audio_source.PlayOneShot(interface_sounds[0]);
    }
    public void playButtonClick()
    {
        audio_source.PlayOneShot(interface_sounds[1]);
    }

    public void playButtonBigClick()
    {
        audio_source.PlayOneShot(interface_sounds[2]);
    }

    public void playPauseSound()
    {
        audio_source.PlayOneShot(interface_sounds[3]);
    }
}
