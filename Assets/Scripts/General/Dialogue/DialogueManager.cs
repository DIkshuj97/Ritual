using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour {

	public TMP_Text nameText;
	public TMP_Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;
	private bool triggerOnce = true;

	public AudioSource aS;
	public DialogueTrigger dialogueTrigger;
	public static bool isDialogueEnded = false;
	//public AudioClip clickClip;
	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		isDialogueEnded = false;
		StartCoroutine(StartingDialogue());
	}

	

	private void Update()
	{
		if (sentences != null)
        {
			if (Input.GetKeyDown(KeyCode.E) && animator.GetBool("IsOpen"))
            {
				//aS.s
				DisplayNextSentence();
				SoundManager.ins.PlayExtraAudio("DialogueSkip", aS);
			}
        }
    }

	IEnumerator StartingDialogue()
    {
		yield return new WaitForSeconds(1f);
		dialogueTrigger.TriggerDialogue();
    }

    public void StartDialogue (Dialogue dialogue)
	{

		if( triggerOnce)
        {
			isDialogueEnded = false;
			PlayerController.playerControl = false;

			triggerOnce = false;

			animator.SetBool("IsOpen", true);

			nameText.text = dialogue.name;

			sentences.Clear();

			foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
        }
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			if (triggerOnce) break;
			SoundManager.ins.PlayExtraAudio("DialogueType", aS);
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
	}
	
	void EndDialogue()
	{
		PlayerController.playerControl = true;
		triggerOnce = true;
		isDialogueEnded = true;
		animator.SetBool("IsOpen", false);
		StartCoroutine(DelayInteract());
	}
	IEnumerator DelayInteract()
	{
		yield return new WaitForSeconds(1);
		PlayerInteract.canInteractAgain = true;
	}
}

