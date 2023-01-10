using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager ins;
    public bool gameIsPaused;

    public GameObject PauseMenu;
    public GameObject gameOverMenu;
    public GameObject handIcon;
    public GameObject bloodScreen;
    public Image batteryBar;
    public GameObject batteryUI;
    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        TurnOffInitially();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            Pause_ResumeGame();
        }

        batteryBar.fillAmount = GameManager.ins.fLightScript.fillvalue;
    }
    void TurnOffInitially()
    {
        PauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        handIcon.SetActive(false);
        bloodScreen.SetActive(false);
        batteryUI.SetActive(false);
    }
    void Pause_ResumeGame()
    {
        if (gameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
