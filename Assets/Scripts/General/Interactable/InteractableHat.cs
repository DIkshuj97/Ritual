using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHat : MonoBehaviour,IInteractable
{
    [TextArea(3, 10)] [SerializeField] private string objectiveText;
    public void Interact()
    {
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        ObjectiveManager.ins.SetText(objectiveText);
        ObjectiveManager.ins.PlayAnim();
        gameObject.SetActive(false);
    }

}
