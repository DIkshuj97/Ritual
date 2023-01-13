using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private GameObject objGO;
    [SerializeField] private TMP_Text objText;
    [SerializeField] private AudioClip objSfx; // play it later using AuidoManager
    
    public static ObjectiveManager ins;
    
    private Animator objAnim;
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
        objAnim = GetComponent<Animator>();
        objGO.SetActive(false);
    }
    public void PlayAnim(GameObject triggerObj=null)
    {
        objAnim.ResetTrigger("Obj");
        trigger = triggerObj;
        objGO.SetActive(true);
        objAnim.SetTrigger("Obj");
    }
    public void TriggerDeactivate()
    {
       if(trigger != null) trigger.SetActive(false);
       objGO.SetActive(false);
    }
    public void SetText(string text)
    {
        objText.text = text;
    }
    
}
