using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pressAnyButton;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject closeButton;

    [SerializeField] Image background;
    [SerializeField] Image backgroundScene;

    [SerializeField] Toggle fullscreen;

    [SerializeField] AudioSource buttonSound;

    private bool anyKeyPressed;
    [SerializeField] float alpha;
    [SerializeField] float sceneAlpha;
    public bool alphaController;
    public bool sceneAlphaController;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.firstTime)
        {
            pressAnyButton.SetActive(false);
            mainMenu.SetActive(true);
            levelSelect.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundAlpha(alphaController);
        SceneBackgroundAlpha(sceneAlphaController);

        PressAnyKeyToStart();

        Screen.fullScreen = fullscreen.isOn;
    }

    void BackgroundAlpha(bool visible)
    {
        if (visible)
        {
            if (alpha < 1)
            {
                alpha += Time.deltaTime;
            }
        }
        else
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime;
            }
        }
        background.color = new Color(background.color.r, background.color.g, background.color.b, alpha);
    }

    void SceneBackgroundAlpha(bool visible)
    {
        if (visible)
        {
            if (sceneAlpha < 1)
            {
                sceneAlpha += Time.deltaTime;
            }
        }
        else
        {
            if (sceneAlpha > 0)
            {
                sceneAlpha -= Time.deltaTime;
            }
        }
        backgroundScene.color = new Color(background.color.r, background.color.g, background.color.b, sceneAlpha);
    }

    void PressAnyKeyToStart()
    {
        if (Input.anyKeyDown && !anyKeyPressed && !GameManager.Instance.firstTime)
        {
            pressAnyButton.SetActive(false);
            mainMenu.SetActive(true);
            anyKeyPressed = true;
        }
    }

    public void Play()
    {
        //mainMenu.SetActive(false);
        buttonSound.Play(0);
        levelSelect.SetActive(true);
    }

    public void Link()
    {
        string url = "https://felpsoliviere.itch.io/";

        Application.OpenURL(url);
    }

    public void LoadScene(int level)
    {
        GameManager.Instance.levelIndex = level;
        buttonSound.Play(0);

        if (GameManager.Instance.levelProgression >= level)
        {
            alphaController = true;
            background.raycastTarget = true;
            StartCoroutine(LoadSceneTimer(1f, level));

        }
        else if (level == 10)
        {
            alphaController = true;
            background.raycastTarget = true;
            StartCoroutine(LoadSceneTimer(1f, level));
        }

        GameManager.Instance.firstTime = true;
    }

    public void DeleteSave()
    {
        StartCoroutine(GameManager.Instance.GameOver(1));
    }

    IEnumerator LoadSceneTimer(float time, int level)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(level);

        StopAllCoroutines();
    }

    public void SettingsMenu()
    {
        settings.SetActive(true);
        buttonSound.Play(0);
    }

    public void Close()
    {
        GameManager.Instance.Save(1);
        settings.SetActive(false);
        buttonSound.Play(0);

    }

    public void Close2()
    {
        levelSelect.SetActive(false);
        buttonSound.Play(0);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
