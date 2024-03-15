using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isDead;
    public bool gameOver;
    public bool firstTime;
    public int levelIndex;
    public bool levelComplete;
    public bool timeIsRunningOut;

    #region Save
    [Header("Save")]
    [Space]
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public int cherrys;
    public int lifes;
    public int levelProgression;
    public int score;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Load(0);
        Load(1);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (lifes == 0)
        {
            gameOver = true;
            StartCoroutine(GameOver(3));
        }

        if (isDead && !gameOver)
        {
            Save(0);
            StartCoroutine(Timer(2));
        }

        LifeGain();
    }

    public void LevelComplete()
    {
        Save(0);

        if (levelProgression == levelIndex)
        {
            levelProgression++;
        }
        Debug.Log("Level Complete");

        StartCoroutine(Timer(3));
    }

    public void LifeGain()
    {
        if (cherrys >= 50)
        {
            cherrys = 0;
            lifes++;
        }
    }

    IEnumerator Timer(int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
        StopAllCoroutines();
    }

    public IEnumerator GameOver(int time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
        cherrys = 0;
        lifes = 3;
        firstTime = false;
        levelProgression = 2;
        score = 0;
        Save(0);

        StopAllCoroutines();
    }

    [System.Serializable]
    class SaveData
    {
        // Local to do the variables
        // public datatype var;

        public int cherrys;
        public int lifes;
        public int levelProgression;
        public int score;
    }

    [System.Serializable]
    class Settings
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
    }

    public void Save(int index)
    {
        SaveData data = new SaveData();
        Settings settings = new Settings();

        if (index == 0)
        {
            //data.var = var;

            data.cherrys = cherrys;
            data.lifes = lifes;
            data.levelProgression = levelProgression;
            data.score = score;

            Debug.Log("Level Data Saved");

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        if (index == 1)
        {
            //settings.var = var;

            settings.masterVolume = masterVolume;
            settings.musicVolume = musicVolume;
            settings.sfxVolume = sfxVolume;

            Debug.Log("Settings Data Saved");

            string json = JsonUtility.ToJson(settings);
            File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
        }
    }

    public void Load(int index)
    {
        string path = Application.persistentDataPath + "/savefile.json";
        string settingsPath = Application.persistentDataPath + "/settings.json";

        if (File.Exists(path) && index == 0)
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //var = data.var;

            cherrys = data.cherrys;
            lifes = data.lifes;
            levelProgression = data.levelProgression;
            score = data.score;
        }

        if (File.Exists(settingsPath) && index == 1)
        {
            string json = File.ReadAllText(settingsPath);
            Settings settings = JsonUtility.FromJson<Settings>(json);

            //var = settings.var;

            masterVolume = settings.masterVolume;
            musicVolume = settings.musicVolume;
            sfxVolume = settings.sfxVolume;
        }
    }
}
