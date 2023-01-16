using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBattery : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        //PlayerInteract.canInteractAgain = true;
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        GameManager.ins.fLightScript.batteries++; 
        gameObject.SetActive(false);
    }
}
