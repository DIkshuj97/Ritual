using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTorch : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        GameManager.ins.player.GetComponent<PlayerController>().UseTorch();
        gameObject.SetActive(false);
    }

    
}
