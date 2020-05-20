using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectMenuBehaviour : MonoBehaviour
{
    public static WeaponSpawnerBehaviour.Track track;
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void loadLucidDream()
    {
        track = WeaponSpawnerBehaviour.Track.LucidDream;
    }
    public void loadSchukran()
    {
        track = WeaponSpawnerBehaviour.Track.Schukran;
    }
    public void loadElTió()
    {
        track = WeaponSpawnerBehaviour.Track.ElTió;
    }
    public void loadRivals()
    {
        track = WeaponSpawnerBehaviour.Track.Rivals;
    }
    public void loadSEKBeat()
    {
        track = WeaponSpawnerBehaviour.Track.SEKBeat;
    }
    public void loadLagom()
    {
        track = WeaponSpawnerBehaviour.Track.Lagom;
    }
    public void loadDeeper()
    {
        track = WeaponSpawnerBehaviour.Track.Deeper;
    }
}
