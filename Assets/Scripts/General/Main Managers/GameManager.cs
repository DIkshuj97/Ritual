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

    [SerializeField] Transform crawlerRespawnPoint;

    [SerializeField] CrawlerRespawn crawlerRespawn;

    [HideInInspector] public GameObject dLight;

    private float intensity = 2f;
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
        dLight = GameObject.FindGameObjectWithTag("Light");
        dLight.SetActive(false);
        dLight.GetComponent<Light>().intensity = intensity;
    }

    public void Respawn()
    {
        player.transform.position = checkpointManager.GetActiveCheckPointPosition();
        playerDeath.RespawnAnim();
        
        if(crawlerRespawn.isReSpawn)
        {
            crawler.transform.position = crawlerRespawnPoint.position;
        }
    }
}
