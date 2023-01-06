using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager ins;
    public GameObject PauseMenu;
    public GameObject handIcon;
    public GameObject bloodScreen;

    public bool gameIsPaused;
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

    }
    void TurnOffInitially()
    {
        PauseMenu.SetActive(false);
        handIcon.SetActive(false);
        bloodScreen.SetActive(false);
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

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
