using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactableknife : MonoBehaviour,IInteractable
{
    private bool triggerOnce = false;
    [SerializeField] CinematicTrigger cinematicTrigger;
    [TextArea(3, 10)] [SerializeField] private string objectiveText;
    public void Interact()
    {
        var dt = GetComponent<DialogueTrigger>();
        if (dt != null) dt.TriggerDialogue();
        triggerOnce = true;
    }

    private void Update()
    {
       if(DialogueManager.isDialogueEnded && triggerOnce)
        {
            triggerOnce = false;
            GameManager.ins.crawler.gameObject.SetActive(true);
            cinematicTrigger.TriggerCutscene();
            GameManager.ins.player.GetComponent<PlayerController>().speed = 15;
            ObjectiveManager.ins.SetText(objectiveText);
            ObjectiveManager.ins.PlayAnim(gameObject);
            gameObject.SetActive(false);
            SoundManager.ins.PlaySfx("CrawlerWake");
        }
    }
}