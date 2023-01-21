using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamDialogue : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            SoundManager.ins.PlayScreamSound();
            var dt = GetComponent<DialogueTrigger>();
            if (dt != null) dt.TriggerDialogue();          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
