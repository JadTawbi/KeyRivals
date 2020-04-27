using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Midi;


public class WeaponSpawnerBehaviour : MonoBehaviour
{
    private MidiFile midi;
    private float ticks;

    // Start is called before the first frame update
    void Start()
    {
        midi = new MidiFile("Assets/Sound/MIDI/LucidDreamMIDI_1.0.mid");
        ticks = midi.DeltaTicksPerQuarterNote;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
