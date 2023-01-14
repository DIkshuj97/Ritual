using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        GetComponent<Animator>().SetBool("Talk", true);
        gameObject.layer = 0;
    }

    private void Update()
    {
        if(DialogueManager.introEnd)
        {
            GetComponent<Animator>().SetBool("Talk", false);
            
        }
    }
}
