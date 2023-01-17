using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;
public class CinematicControlRemover : MonoBehaviour
{
    public bool isLastCinematic;
    public static bool cinematicPlaying;
    public Animator cinematicAnimator;

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
        cinematicAnimator.SetBool("On",true);
        PlayerController.playerControl = false;
        cinematicPlaying = true;
        if (isLastCinematic) 
        {
            GameManager.ins.crawler.gameObject.SetActive(false);
        }
    }

    void EnableControl(PlayableDirector pd)
    {
        cinematicAnimator.SetBool("On",false);
        PlayerController.playerControl = true;
        cinematicPlaying = false;
        if (isLastCinematic)
        {
            GameManager.ins.crawler.gameObject.SetActive(true);
        }
    }
}
