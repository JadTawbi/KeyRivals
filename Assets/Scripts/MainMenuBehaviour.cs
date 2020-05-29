using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public static float audio_lag, video_lag;
    public AudioSource audio_source;
    public enum MainMenuTrack { Neon, PickMyBrain };

    private void Start()
    {
        loadTrack((MainMenuTrack)Random.Range(0,2));
        audio_source.Play();
        CharacterSelectMenuBehaviour.randomizeCharacters();
    }
    private void loadTrack(MainMenuTrack track_to_load)
    {
        switch (track_to_load)
        {
            case MainMenuTrack.Neon:
                audio_source.clip = Resources.Load("Music/Menu/NeonLoudBright6bit") as AudioClip;
                break;
            case MainMenuTrack.PickMyBrain:
                audio_source.clip = Resources.Load("Music/Menu/PickMyBrainLoudBright16bit") as AudioClip;
                break;
        }
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
