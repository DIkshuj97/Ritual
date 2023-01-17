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

    private Animator tutAnim;
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
        tutAnim = GetComponent<Animator>();
        tutGO.SetActive(false);
    }
    public void PlayAnim(GameObject triggerObj = null)
    {
        tutAnim.ResetTrigger("Tutorial");
        trigger = triggerObj;
        tutGO.SetActive(true);
        tutAnim.SetTrigger("Tutorial");
    }
    public void TriggerDeactivate()
    {
        if (trigger != null) trigger.SetActive(false);
        tutGO.SetActive(false);
    }
    public void SetText(string text)
    {
        tutText.text = text;
    }
}
