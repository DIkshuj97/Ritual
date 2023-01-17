using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBattery : MonoBehaviour, IInteractable
{
    [TextArea(3, 10)] [SerializeField] private string tutorialText;

    public void Interact()
    {
        TutorialManager.ins.SetText(tutorialText);
        TutorialManager.ins.TutorialActivate(gameObject);
        //PlayerInteract.canInteractAgain = true;
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        GameManager.ins.fLightScript.batteries++; 
        gameObject.SetActive(false);
    }
}
