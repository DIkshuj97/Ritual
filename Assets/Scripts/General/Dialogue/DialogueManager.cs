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

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}


	private void Update()
	{
		if (sentences != null)
        {
			if (Input.GetKeyDown(KeyCode.E))
            {
				DisplayNextSentence();
			}
        }
    }
    public void StartDialogue (Dialogue dialogue)
	{
		if( triggerOnce)
        {
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
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		triggerOnce = true;
		animator.SetBool("IsOpen", false);
	}

}
