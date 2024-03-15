using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifesText;
    [SerializeField] TextMeshProUGUI EndGameText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI cherysText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Image background;

    [SerializeField] GameObject gameplayMenu;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] AudioSource buttonSound;
    [SerializeField] AudioSource timeRunningOutAudio;

    [SerializeField] float time;
    [SerializeField] float alpha;

    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        if (GameManager.Instance.levelComplete || GameManager.Instance.isDead || GameManager.Instance.gameOver)
        {
            lifesText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
            cherysText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);

            visible = true;

            if (GameManager.Instance.gameOver)
            {
                EndGameText.alpha = alpha;
            }

        }
        else
        {
            Timer();
            Menu();
        }

        BackgroundAlpha(visible);

        scoreText.text = GameManager.Instance.score.ToString();
        lifesText.text = GameManager.Instance.lifes.ToString();
        cherysText.text = GameManager.Instance.cherrys.ToString();
    }

    public void BackgroundAlpha(bool visible)
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

    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameplayMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Timer()
    {
        if (Mathf.Round(time) <= 0 && !GameManager.Instance.isDead)
        {
            GameManager.Instance.lifes--;
            GameManager.Instance.isDead = true;
        }
        else if (!GameManager.Instance.isDead /*&& !GameManager.Instance.levelComplete*/)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.Round(time).ToString();

            if (time <= 20 && !GameManager.Instance.timeIsRunningOut)
            {
                timeText.color = Color.red;
                timeRunningOutAudio.Play();
                GameManager.Instance.timeIsRunningOut = true;
            }

            if (GameManager.Instance.timeIsRunningOut && !GameManager.Instance.isDead)
            {
                timeText.fontSize += Time.deltaTime * 3;
            }
        }

    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        buttonSound.Play(0);
    }

    public void CloseSettigns()
    {
        settingsMenu.SetActive(false);
        GameManager.Instance.Save(1);
        buttonSound.Play(0);
    }

    public void CloseMenu()
    {
        gameplayMenu.SetActive(false);
        buttonSound.Play(0);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        buttonSound.Play(0);
        Time.timeScale = 1;
    }
}
