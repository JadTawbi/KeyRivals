using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvasBehaviour : MonoBehaviour
{
    public UISoundsBehaviour UI_sounds_behaviour;

    private void OnEnable()
    {
        UI_sounds_behaviour.playPauseSound();
    }
}
