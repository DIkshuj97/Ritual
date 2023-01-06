using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) TriggerDeath();
    }   

    public void TriggerDeath()
    {
        anim.SetTrigger("Death");
        UIManager.ins.bloodScreen.SetActive(true);
    }
}
