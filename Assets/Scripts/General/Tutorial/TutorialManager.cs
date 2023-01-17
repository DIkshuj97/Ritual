using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutGO;
    [SerializeField] private TMP_Text tutText;
    [SerializeField] private AudioClip tutSfx; // play it later using AuidoManager

    public static TutorialManager ins;
    private GameObject trigger;

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tutGO.SetActive(false);
    }
    public void TutorialActivate(GameObject triggerObj = null)
    {    
        trigger = triggerObj;
        tutGO.SetActive(true);
    }
    public void TutorialDeactivate()
    {
        if (trigger != null) trigger.SetActive(false);
        tutGO.SetActive(false);
    }
    public void SetText(string text)
    {
        tutText.text = text;
    }
}
