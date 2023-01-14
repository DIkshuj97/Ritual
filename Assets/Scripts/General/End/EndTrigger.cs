using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.ins.crawler.canChase = false;
        FindObjectOfType<Fader>().FadeToLevel();
    }
}
