using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	private bool trigger = false;
	public void TriggerDialogue ()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

    private void OnTriggerEnter(Collider other)
    {

		if(!trigger)
        {
			TriggerDialogue();
			trigger = true;
		}
		

	}
}
