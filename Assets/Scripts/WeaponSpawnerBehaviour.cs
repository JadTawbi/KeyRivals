using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Midi;
using System.IO;
using UnityEngine.SceneManagement;

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

    public enum PlayableTrack { LucidDream, Schukran, ElTio, Rivals, SEKBeat, Lagom, Deeper, Practice};
    private TextAsset midi_as_text;

    public bool has_song_started, has_weapon_spawn_started;

    public GameObject score_player1, score_player2;
    private ScoreBehaviour score_behaviour_p1, score_behaviour_p2;

    private bool audio_lag_done, video_lag_done, lag_timers_on;
    private float audio_lag_timer, video_lag_timer, audio_lag, video_lag;

    public GameObject boss_eye, boss;
    private Animator boss_eye_animator, boss_animator;

    public static bool color_locked_player1 = false, color_locked_player2 = false;
    public static bool color_random_player1 = false, color_random_player2 = false;
    WeaponBehaviour.AttackColour last_color;

    [System.NonSerialized]
    public bool poker_chip_active;

    // Start is called before the first frame update
    void Start()
    {
        loadTrack(SongSelectMenuBehaviour.track);

        audio_source.volume = PlayerPrefs.GetFloat("volume", OptionsMenuBehaviour.DEFAULT_VOLUME);

        Stream midi_as_memory_stream = new MemoryStream(midi_as_text.bytes);
        midi_file = new MidiFile(midi_as_memory_stream, true);
        ticks_per_quarter_note = midi_file.DeltaTicksPerQuarterNote;
        spawn_timer = 0.0f;
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

        has_song_started = has_weapon_spawn_started = false;

        score_behaviour_p1 = score_player1.GetComponent<ScoreBehaviour>();
        score_behaviour_p2 = score_player2.GetComponent<ScoreBehaviour>();

        GameBehaviour.paused = false;

        audio_lag_done = video_lag_done = lag_timers_on = false;
        audio_lag_timer = video_lag_timer = 0.0f;
        audio_lag = PlayerPrefs.GetFloat("audio lag", 0.0f);
        video_lag = PlayerPrefs.GetFloat("video lag", 0.0f);

        boss_animator = boss.GetComponent<Animator>();
        boss_eye_animator = boss_eye.GetComponent<Animator>();
        boss_eye_animator.speed = beats_per_minute / 60.0f;

        poker_chip_active = false;
    }
    private void loadTrack(PlayableTrack track_to_load)
    {
        switch (track_to_load)
        {
            case PlayableTrack.LucidDream:
                midi_as_text = Resources.Load("MIDI/LucidDreamMIDI_1.1") as TextAsset;   //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/LucidDreamLoudBright16bit") as AudioClip;
                beats_per_minute = 90;
                break;
            case PlayableTrack.Schukran:
                midi_as_text = Resources.Load("MIDI/SchukranMIDI_1.2") as TextAsset;   //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/SchukranLoudBrightLessbass16bit") as AudioClip;
                beats_per_minute = 90;
                break;
            case PlayableTrack.ElTio:
                midi_as_text = Resources.Load("MIDI/ElTióMIDI_2.0") as TextAsset;   //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/ElTióLoudBrightWarm16bit") as AudioClip;
                beats_per_minute = 125;
                break;
            case PlayableTrack.Rivals:
                midi_as_text = Resources.Load("MIDI/RivalsMIDI_2.2") as TextAsset;  //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/RivalsBright16bit") as AudioClip;
                beats_per_minute = 120;
                break;
            case PlayableTrack.SEKBeat:
                midi_as_text = Resources.Load("MIDI/SEKBeatMIDI_2.0") as TextAsset;  //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/SEKBeatBrightWarm16bit") as AudioClip;
                beats_per_minute = 150;
                break;
            case PlayableTrack.Lagom:
                midi_as_text = Resources.Load("MIDI/LagomMIDI_2.0") as TextAsset;  //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/LagomLoudBright16bit") as AudioClip;
                beats_per_minute = 150;
                break;
            case PlayableTrack.Deeper:
                midi_as_text = Resources.Load("MIDI/DeeperMIDI_2.0") as TextAsset;  //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/DeeperBrightLessBassLoud16bit") as AudioClip;
                beats_per_minute = 150;
                break;
            case PlayableTrack.Practice:
                midi_as_text = Resources.Load("MIDI/PracticeMIDI_1.0") as TextAsset;    //MIDI file extension changed to .bytes manually
                audio_source.clip = Resources.Load("Music/Practice Room -21_5") as AudioClip;
                beats_per_minute = 104;
                break;

        }

        spawn_offset = (60.0f / beats_per_minute) * 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehaviour.paused == false)
        {
            if (notes.Count > 0)
            {
                if (has_weapon_spawn_started == true)
                {
                    spawnWeapons();
                }
            }
            
            if (audio_source.isPlaying == false && has_song_started == true)
            {
                WinScreenBehaviour.player1_score = score_behaviour_p1.score_amount;
                WinScreenBehaviour.player2_score = score_behaviour_p2.score_amount;
                SceneManager.LoadScene("WinScreen");
            }
            checkLagTimers();
        }        
    }

    private void spawnWeapons()
    {
        if (spawn_timer + spawn_offset >= notes[0].spawn_time_in_seconds)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject poker_chip_in_lane = null;
                if (poker_chip_active == true)
                {
                    switch (i)
                    {
                        case 0:
                            poker_chip_in_lane = GameObject.FindWithTag("PokerChip_Player1_" + notes[0].lane);
                            break;
                        case 1:
                            poker_chip_in_lane = GameObject.FindWithTag("PokerChip_Player2_" + notes[0].lane);
                            break;
                    }
                }
                if (poker_chip_in_lane == null)
                {
                    GameObject new_weapon = Instantiate(weapon_prefab, transform);
                    new_weapon.GetComponent<SpriteRenderer>().sortingOrder = notes_displayed;

                    WeaponBehaviour new_weapon_behaviour = new_weapon.GetComponent<WeaponBehaviour>();
                    new_weapon_behaviour.lane = notes[0].lane;
                    new_weapon_behaviour.attack_colour = notes[0].attack_colour;
                    new_weapon_behaviour.grow_interval = spawn_offset; //To-Do: Set it based on the song's BPM
                    switch (i)
                    {
                        case 0:
                            if (color_locked_player1)
                            {
                                new_weapon_behaviour.attack_colour = last_color;
                            }
                            else if (color_random_player1)
                            {
                                new_weapon_behaviour.attack_colour = (WeaponBehaviour.AttackColour)Random.Range(0, 4);
                            }
                            new_weapon_behaviour.side = WeaponBehaviour.Side.Player1;
                            break;
                        case 1:
                            if (color_locked_player2)
                            {
                                new_weapon_behaviour.attack_colour = last_color;
                            }
                            else if (color_random_player2)
                            {
                                new_weapon_behaviour.attack_colour = (WeaponBehaviour.AttackColour)Random.Range(0, 4);
                            }
                            new_weapon_behaviour.side = WeaponBehaviour.Side.Player2;
                            break;
                    }
                    if (color_locked_player1 == false && color_locked_player2 == false)
                    {
                        last_color = new_weapon_behaviour.attack_colour; //last note color cached for the powerup
                    }
                }
            }

            notes.Remove(notes[0]);
            notes_displayed++;
        }

        spawn_timer += Time.deltaTime;
    }

    private void startSong()
    {
        audio_source.Play();
        has_song_started = true;
        boss_animator.SetTrigger("startMoving");
    }
    private void startWeaponSpawn()
    {
        has_weapon_spawn_started = true;
    }

    public void startLagTimers()
    {
        lag_timers_on = true;
    }

    private void checkLagTimers()
    {
        if (lag_timers_on == true)
        {
            if (audio_lag_done == false && audio_lag_timer >= audio_lag)
            {
                audio_lag_done = true;
                startWeaponSpawn();
            }
            else if (audio_lag_timer < audio_lag)
            {
                audio_lag_timer += Time.deltaTime;
            }

            if (video_lag_done == false && video_lag_timer >= video_lag)
            {
                video_lag_done = true;
                startSong();
            }
            else if (video_lag_timer < video_lag)
            {
                video_lag_timer += Time.deltaTime;
            }

            if (audio_lag_done == true && video_lag_done == true)
            {
                lag_timers_on = false;
            }
        }
    }
}
