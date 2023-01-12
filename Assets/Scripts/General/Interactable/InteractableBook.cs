using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBook : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        
    }
}
