using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    enum State
    {
        Playing,
        Pause,
        Setting,
        End
    }
    State gameState = State.Playing;
    public GameObject pauseMenuPanel;
    public GameObject settingPanel;
    public GameObject endPanelWin;
    public GameObject endPanelLose;

    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PackageText;

    public Image EndWinLose;
    public Sprite EndWin;
    public Sprite EndLose;
    public TextMeshProUGUI EndScoreText;
    public TextMeshProUGUI EndSPackageText;


    public int minLeft = 0;
    public int secondLeft = 10;
    public bool timeGone = false;

    public int droneHealth;
    public int gameScore;
    public int deliveredPackage;

    public Slider healthBar;

    public AudioMixer audioMixer;


    // Start is called before the first frame update
    void Start()
    {
        if (timeDisplay != null)
        {
            timeDisplay.text = "0" + minLeft + ":" + secondLeft;
        }
    }

    // Update is called once per frame



    public void OnGamePause(InputValue val)
    {
        switch (gameState)
        {
            case State.Playing:
                Pause();
                break;
            case State.Pause:
                Resume();
                break;
            case State.Setting:
                ReturnFromSetting();
                break;
            case State.End:
                LoadMainMenu();
                break;

        }

    }

    public void GameEnd(bool win)
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameState = State.End;
        pauseMenuPanel.SetActive(false);
        if (win)
        {
            endPanelWin.SetActive(true);
        }
        else
        {
            endPanelLose.SetActive(true);
        }

        //EndScoreText.text = gameScore.ToString();
        //EndSPackageText.text = deliveredPackage.ToString();  
        //settingPanel.SetActive(false);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        gameState = State.Playing;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        gameState = State.Pause;
    }

    public void GoSettingMenu()
    {
        pauseMenuPanel.SetActive(false);
        settingPanel.SetActive(true);
        gameState = State.Setting;
    }

    public void ReturnFromSetting()
    {
        settingPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gameState = State.Pause;
    }

    public void LoadMainMenu()
    {
        gameState = State.End;
        SceneManager.LoadScene(0);
    }

    public void PlayScene()
    {
        Time.timeScale = 1f;
        gameState = State.Playing;
        SceneManager.LoadScene(1);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        gameState = State.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        gameState = State.End;
        Application.Quit();
    }


    public void UpdateGUI(int score, int delivered)
    {
        gameScore = score;
        deliveredPackage = delivered;
        ScoreText.text = gameScore.ToString("F0");
        PackageText.text = deliveredPackage.ToString();
    }
    public void SetVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }
}
