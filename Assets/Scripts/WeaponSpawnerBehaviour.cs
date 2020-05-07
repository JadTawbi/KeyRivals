﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Midi;
using System.IO;
using System.Linq;


public class WeaponSpawnerBehaviour : MonoBehaviour
{
    private MidiFile midi_file;
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

            lane = (WeaponBehaviour.Lane)Mathf.Abs(note_number - 3); // lanes in the game are in reverse order than the midi file, so we grabbed the absolute value of the negative numbers to reverse them

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
    
    private float spawn_timer;
    [System.NonSerialized]
    public float spawn_offset;
    public GameObject weapon_prefab;

    public AudioSource audio_source;

    private int notes_displayed;

    public enum Track { LucidDream, Schukran, ElTió };
    private TextAsset midi_as_text;

    // Start is called before the first frame update
    void Start()
    {
        loadTrack(Track.Schukran);

        Stream midi_as_memory_stream = new MemoryStream(midi_as_text.bytes);
        midi_file = new MidiFile(midi_as_memory_stream, true);
        ticks_per_quarter_note = midi_file.DeltaTicksPerQuarterNote;
        spawn_timer = 0.0f;
        spawn_offset = 1.0f; //Change based on song
        notes = new List<Note>();

        foreach (MidiEvent midi_event in midi_file.Events[0]) //To-Do: Replace midi_file.Events[0] for midi_events
        {
            if (midi_event.CommandCode == MidiCommandCode.NoteOn)
            {
                NoteOnEvent note_on_event = (NoteOnEvent)midi_event;
                Note note = new Note(note_on_event.AbsoluteTime, note_on_event.NoteNumber, note_on_event.Velocity);
                notes.Add(note);
            }
        }

        notes_displayed = 0;


        audio_source.Play();
    }
    private void loadTrack(Track track_to_load)
    {
        switch (track_to_load)
        {
            case Track.LucidDream:
                midi_as_text = Resources.Load("MIDI/LucidDreamMIDI_1.0") as TextAsset;   //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/Lucid Dream - Full 17_4 Mix") as AudioClip;
                beats_per_minute = 90;
                break;
            case Track.Schukran:
                midi_as_text = Resources.Load("MIDI/SchukranMIDI_1.1") as TextAsset;   //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/Schukran (شكراً) Full - 26_4") as AudioClip;
                beats_per_minute = 90;
                break;
            case Track.ElTió:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (notes.Count > 0)
        {
            spawnWeapons();
        }
    }

    private void spawnWeapons()
    {
        if (spawn_timer + spawn_offset >= notes[0].spawn_time_in_seconds)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject new_weapon = Instantiate(weapon_prefab, transform);
                new_weapon.GetComponent<SpriteRenderer>().sortingOrder = notes_displayed;

                WeaponBehaviour new_weapon_behaviour = new_weapon.GetComponent<WeaponBehaviour>();
                new_weapon_behaviour.lane = notes[0].lane;
                new_weapon_behaviour.attack_colour = notes[0].attack_colour;
                new_weapon_behaviour.grow_interval = spawn_offset; //To-Do: Set it based on the song's BPM
                switch(i)
                {
                    case 0:
                        new_weapon_behaviour.side = WeaponBehaviour.Side.Player1;
                        break;
                    case 1:
                        new_weapon_behaviour.side = WeaponBehaviour.Side.Player2;
                        break;
                }
            }

            notes.Remove(notes[0]);
            notes_displayed++;
        }

        spawn_timer += Time.deltaTime;
    }

}
