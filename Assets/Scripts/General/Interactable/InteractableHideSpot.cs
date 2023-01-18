using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHideSpot : MonoBehaviour,IInteractable
{
    public bool isHide = false;

    public void Interact()
    {
        if(!isHide)
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0,0,-4));
            GameManager.ins.player.transform.Rotate(new Vector3(0,180,0));
           GetComponent<BoxCollider>().isTrigger = false;
            isHide = true;
        }
        else
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0, 0, 5));
            GetComponent<BoxCollider>().isTrigger = false;
            isHide = false;
        }
    }

   
}
