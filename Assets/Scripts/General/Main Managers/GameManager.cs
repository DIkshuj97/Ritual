using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    [HideInInspector] public GameObject player;

    [HideInInspector] public FlashlightAdvanced fLightScript; //Get Player Controller

    public AIController crawler;
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
        fLightScript = player.GetComponent<PlayerController>().flashLightScript;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
  
}
