using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHideSpot : MonoBehaviour
{
    public bool isHide = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if(Input.GetKeyDown("e"))
            {
                Interact();
            }
           
        }
    }

    public void Interact()
    {
        if(!isHide)
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0,0,-3));
            PlayerController.isOnlyLook = true;
           GetComponent<BoxCollider>().isTrigger = false;
            isHide = true;
        }
        else
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0, 0, 4));
            PlayerController.isOnlyLook = false;
            GetComponent<BoxCollider>().isTrigger = false;
            isHide = false;
        }
    }

   
}
