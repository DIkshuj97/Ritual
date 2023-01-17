using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;

    [HideInInspector] public GameObject player;

    [HideInInspector] public FlashlightAdvanced fLightScript;

    [HideInInspector] public AIController crawler;

    [HideInInspector] public CheckPointManager checkpointManager;

    [HideInInspector] public PlayerDeath playerDeath;
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
        Initiallizer();
    }

    private void Initiallizer()
    {
        player = GameObject.FindWithTag("Player");
        crawler = GameObject.FindWithTag("Crawler").GetComponent<AIController>();
        GameObject.FindWithTag("Crawler").SetActive(false);
        fLightScript = player.GetComponent<PlayerController>().flashLightScript;
        checkpointManager = GetComponent<CheckPointManager>();
        playerDeath = player.GetComponent<PlayerDeath>();
    }

    public void Respawn()
    {
        player.transform.position = checkpointManager.GetActiveCheckPointPosition();
        playerDeath.RespawnAnim();
    }
}
