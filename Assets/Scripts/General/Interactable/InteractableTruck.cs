using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTruck : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject flashLight;
    public void Interact()
    {
        TutorialManager.ins.TutorialDeactivate();
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        gameObject.layer = 0;
        flashLight.layer = 8;
    }

}
