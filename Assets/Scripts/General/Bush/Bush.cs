using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRend;
    [SerializeField] private Material transparentMaterial;
    Material originalMat;
  
    // Start is called before the first frame update
    void Start()
    {
        originalMat = meshRend.sharedMaterial;
    }
    
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) SetModeTransparent();

        TutorialManager.ins.TutorialDeactivate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) RevertMode();
    }
    private void SetModeTransparent()
    {
        SoundManager.ins.PlayBush();
        meshRend.sharedMaterial = transparentMaterial;
        UIManager.ins.ChangeHideBushImage(true);
    }
    private void RevertMode()
    {
        SoundManager.ins.PlayBush();
        meshRend.sharedMaterial = originalMat;
        UIManager.ins.ChangeHideBushImage(false);
    }
}
