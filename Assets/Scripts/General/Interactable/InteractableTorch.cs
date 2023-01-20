using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTorch : MonoBehaviour,IInteractable
{
    [TextArea(3, 10)] [SerializeField] private string tutorialText;

    public void Interact()
    {
        GameManager.ins.flashLightPicked = true;
        SoundManager.ins.PlayEquip();
        TutorialManager.ins.SetText(tutorialText);
        TutorialManager.ins.TutorialActivate(gameObject);
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        GameManager.ins.player.GetComponent<PlayerController>().UseTorch();
        gameObject.SetActive(false);
    }

    
}
