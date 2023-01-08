using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public GameObject player;

    public FlashlightAdvanced fLightScript; //Get Player Controller
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
