using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource loseAudio;

    public AudioSource enemyDeathSFX;

    [SerializeField] AudioSource[] sfxAudio;
    [SerializeField] AudioSource[] musicAudio;

    int gameOverIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Find all GameObjects with AudioSource components in the scene:
        var allAudioSources = FindObjectsOfType<AudioSource>();

        // Filter for AudioSources with "SFX" in the clip name:
        sfxAudio = allAudioSources.Where(source => source.clip != null && source.clip.name.Contains("SFX")).ToArray();
        musicAudio = allAudioSources.Where(source => source.clip != null && source.clip.name.Contains("Music")).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        sfxAudio = sfxAudio.Where(source => source != null).ToArray();
        musicAudio = musicAudio.Where(source => source != null).ToArray();

        foreach (AudioSource sfx in sfxAudio)
        {
            sfx.volume = GameManager.Instance.sfxVolume * GameManager.Instance.masterVolume;
        }

        foreach (AudioSource music in musicAudio)
        {
            music.volume = GameManager.Instance.musicVolume * GameManager.Instance.masterVolume;
        }

        if (GameManager.Instance.isDead || GameManager.Instance.gameOver || GameManager.Instance.levelComplete)
        {
            musicAudioSource.Stop();
        }

        if (GameManager.Instance.gameOver && gameOverIndex == 0)
        {
            loseAudio.Play(0);
            gameOverIndex = 1;
        }

        if (GameManager.Instance.timeIsRunningOut)
        {
            musicAudioSource.pitch = 1.3f;
        }
    }
}
