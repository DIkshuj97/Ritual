using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public TMP_Text batteryText;
    public Image flashLightImage;

    public Sprite flashLightOFF;
    public Sprite flashLightON;

    public Image hideBushImage;

    public Sprite hidingOn;
    public Sprite hidingOff;
    public GameObject SaveUI;
    public RectTransform minimapBorder;
    public GameObject aim;
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
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerDeath.isAlive)
        {
            gameIsPaused = !gameIsPaused;
            Pause_ResumeGame();
        }
        //batteryText.text = "Batteries : "+(GameManager.ins.fLightScript.batteries+1).ToString();
        batteryBar.fillAmount = GameManager.ins.fLightScript.fillvalue;


    }
     public void GameOverUI(bool value)
     {
        Cursor.visible = value;
        gameOverMenu.SetActive(value);
     }
   
    void TurnOffInitially()
    {
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        handIcon.SetActive(false);
        bloodScreen.SetActive(false);
        batteryUI.SetActive(false);
        SaveUI.SetActive(false);
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
        SoundManager.ins.PlayPress();
        Cursor.visible = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    public void Resume()
    {
        SoundManager.ins.PlayPress();
        gameIsPaused = false;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    public void Restart()
    {
        SoundManager.ins.PlayPress();
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameOverUI(false);
        bloodScreen.SetActive(false);
        GameManager.ins.Respawn();        
    }
    public void Menu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }
  
    public void Quit()
    {
        SoundManager.ins.PlayPress();
        Application.Quit();
    }

    public void ChangeFlashLightImage(bool isOn)
    {
        if(isOn)
        {
            flashLightImage.sprite = flashLightON;
        }
        else
        {
            flashLightImage.sprite = flashLightOFF;
        }
    }

    public void ChangeHideBushImage(bool isHide)
    {
        hideBushImage.gameObject.SetActive(true);

        if (isHide)
        {

            hideBushImage.sprite = hidingOn;
        }
        else
        {
            hideBushImage.sprite = hidingOff;
        }
    }

    public IEnumerator ShowSaveUI()
    {
        SaveUI.SetActive(true);
        yield return new WaitForSeconds(3);
        SaveUI.SetActive(false);

    }

}
