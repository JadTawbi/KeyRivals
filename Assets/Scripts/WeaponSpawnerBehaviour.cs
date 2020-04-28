﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Midi;


public class WeaponSpawnerBehaviour : MonoBehaviour
{
    private MidiFile midi;
    [System.NonSerialized]
    public static int ticks_per_quarter_note, beats_per_minute;

    public class Note 
    {
        public float spawn_time_in_seconds;
        public WeaponBehaviour.Lane lane;
        public WeaponBehaviour.AttackColour attack_colour;
        public Note(float spawn_time_in_ticks, int note_number, int velocity)
        {
            spawn_time_in_seconds = (60 * spawn_time_in_ticks) / (ticks_per_quarter_note * beats_per_minute);

            lane = (WeaponBehaviour.Lane)Mathf.Abs(note_number - 42);

            switch(velocity)
            {
                case 127:
                    attack_colour = WeaponBehaviour.AttackColour.Red;
                    break;

                case 77:
                    attack_colour = WeaponBehaviour.AttackColour.Green;
                    break;

                case 35:
                    attack_colour = WeaponBehaviour.AttackColour.Blue;
                    break;

                case 94:
                    attack_colour = WeaponBehaviour.AttackColour.Yellow;
                    break;
            }
        }
    }

    List<Note> notes;

    // Start is called before the first frame update

    void Start()
    {
        midi = new MidiFile("Assets/Sound/MIDI/LucidDreamMIDI_1.0.mid");  //   File format: 0     Tracks: 1      Ticks: 480     Song lenght: 2:23
        ticks_per_quarter_note = midi.DeltaTicksPerQuarterNote;
        beats_per_minute = 90;
        notes = new List<Note>();
        foreach (MidiEvent midi_event in midi.Events[0])
        {
            if (midi_event.CommandCode == MidiCommandCode.NoteOn)
            {
                NoteOnEvent note_on_event = (NoteOnEvent)midi_event;
                Note note = new Note(note_on_event.AbsoluteTime, note_on_event.NoteNumber, note_on_event.Velocity);
                notes.Add(note);
            }
        }
        foreach (Note note in notes)
        {
            Debug.Log("Spawn time is: " + note.spawn_time_in_seconds.ToString() + " seconds, the Lane is " + note.lane.ToString() + " and the color is: " + note.attack_colour.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}