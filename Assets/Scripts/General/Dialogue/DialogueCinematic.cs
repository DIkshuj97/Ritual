using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueCinematic : MonoBehaviour
{
	public TMP_Text nameText;
	public TMP_Text dialogueText;

	public Animator animator;

	public Dialogue dialogueCine;

	private Queue<string> sentences;


	public AudioSource aS;


	void Start()
	{
		sentences = new Queue<string>();

		StartCoroutine(StartingDialogue());
	}

	private void Update()
	{
        //if (sentences != null)
        //{
        //    if (Input.GetKeyDown(KeyCode.E) && animator.GetBool("IsOpen"))
        //    {
        //        aS.s

        //        DisplayNextSentence();
        //        SoundManager.ins.PlayExtraAudio("DialogueSkip", aS);
        //    }
        //}
    }

	IEnumerator StartingDialogue()
	{
		yield return new WaitForSeconds(1f);
		StartDialogue(dialogueCine);
	}

	public void StartDialogue(Dialogue dialogue)
	{

			animator.SetBool("IsOpen", true);

			nameText.text = dialogue.name;

			sentences.Clear();

			foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		
	}

	public void DisplayNextSentence()
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

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			aS.Play();
			//SoundManager.ins.PlayExtraAudio("DialogueType", aS);
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.03f);
		}
		yield return new WaitForSeconds(1f);
		DisplayNextSentence();
	}

	void EndDialogue()
	{
		PlayerController.playerControl = true;
		
		animator.SetBool("IsOpen", false);
		
	}
}
