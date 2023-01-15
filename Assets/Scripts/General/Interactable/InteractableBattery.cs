using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBattery : MonoBehaviour, IInteractable
{
    public void Interact()
    {

        GameManager.ins.fLightScript.batteries++; 
        gameObject.SetActive(false);
    }
}
