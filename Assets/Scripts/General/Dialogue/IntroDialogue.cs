using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] GameObject continuedText;
    private void Update()
    {
        if(DialogueManager.isDialogueEnded)
        {
            continuedText.SetActive(true);
           
        }
    }
}
