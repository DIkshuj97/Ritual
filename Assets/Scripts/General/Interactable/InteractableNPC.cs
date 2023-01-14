using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour,IInteractable
{
    private bool interactWithNPC = false;
    public void Interact()
    {
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        GetComponent<Animator>().SetBool("Talk", true);
        gameObject.layer = 0;
        interactWithNPC = true;
    }

    private void Update()
    {
        if(DialogueManager.isDialogueEnded && interactWithNPC)
        {
            GetComponent<Animator>().SetBool("Talk", false);
            interactWithNPC = false;
        }
    }
}
