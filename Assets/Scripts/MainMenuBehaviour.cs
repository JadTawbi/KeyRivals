using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public static float audio_lag, video_lag;

    private void Start()
    {
        CharacterSelectMenuBehaviour.randomizeCharacters();
        OptionsMenuBehaviour.volume_value = 0.5f;
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
