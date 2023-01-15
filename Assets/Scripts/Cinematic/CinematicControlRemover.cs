using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;
public class CinematicControlRemover : MonoBehaviour
{
    public bool isLastCinematic;
    public static bool cinematicPlaying = false;
    private void Awake()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    private void OnEnable()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    private void OnDisable()
    {
        GetComponent<PlayableDirector>().played -= DisableControl;
        GetComponent<PlayableDirector>().stopped -= EnableControl;
    }
    void DisableControl(PlayableDirector pd)
    {
        PlayerController.playerControl = false;
        cinematicPlaying = true;
        if (isLastCinematic) 
        {
            GameManager.ins.crawler.gameObject.SetActive(false);
        }
    }

    void EnableControl(PlayableDirector pd)
    {
        PlayerController.playerControl = true;
        cinematicPlaying = false;
        if (isLastCinematic)
        {
            GameManager.ins.crawler.gameObject.SetActive(true);
        }
    }
}
