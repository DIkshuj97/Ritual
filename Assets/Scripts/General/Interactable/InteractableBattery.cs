using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBattery : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        PlayerInteract.canInteractAgain = true;
        GameManager.ins.fLightScript.batteries++; 
        gameObject.SetActive(false);
    }
}
