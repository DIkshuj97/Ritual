using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialogue : MonoBehaviour
{
    private void Update()
    {
        if(DialogueManager.introEnd)
        {
            FindObjectOfType<Fader>().FadeToLevel();
        }
    }
}
