using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [TextArea(3, 10)] [SerializeField] private string tutorialText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.ins.SetText(tutorialText);
            TutorialManager.ins.PlayAnim(gameObject);
        }
    }
}
