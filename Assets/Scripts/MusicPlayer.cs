using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public enum musicID
    {
        GameOver = 0,
        OverworldPeaceful = 1,
        OverworldBattle = 2
    }

    static MusicPlayer instance = null;

    public static MusicPlayer GetInstance()
    {
        return instance;
    }

    [SerializeField] List<AudioClip> music;

    private AudioSource audioSource;
    private void Awake()
    {
        if (instance != null) return;
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();

        instance = this;
    }
    public void ChangeMusic(musicID id)
    {
        StopMusic();
        audioSource.clip = music[Convert.ToInt32(id)];
        PlayMusic();
    }
    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
