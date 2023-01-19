using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHideSpot : MonoBehaviour
{
    public bool isHide = false;
    [SerializeField] BoxCollider boxCollider;
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
        
        if (!isHide)
        {
            SoundManager.ins.PlayToilet();
            boxCollider.isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0,0,-4));
            PlayerController.isOnlyLook = true;
            boxCollider.isTrigger = false;
            isHide = true;
        }
        else
        {
            boxCollider.isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0, 0, 4));
            PlayerController.isOnlyLook = false;
            boxCollider.isTrigger = false;
            isHide = false;
        }
    }

   
}
