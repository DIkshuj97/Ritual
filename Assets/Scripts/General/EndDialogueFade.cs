using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDialogueFade : MonoBehaviour
{
    public void EndCredits()
    {
        FindObjectOfType<Fader>().FadeToLevel();
    }
}
