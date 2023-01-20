using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHideSpot : MonoBehaviour, IInteractable
{
    bool inside = false;
    bool outside = true;
    [SerializeField] BoxCollider boxCollider;

    public void Interact()
    {
        if (outside && !inside)
        {
            outside = false;
            inside = true;
            
            SoundManager.ins.PlayToiletIn();
            GameManager.ins.player.GetComponent<CharacterController>().enabled = false;
            boxCollider.isTrigger = true;
            Vector3 pos = new Vector3(transform.position.x, GameManager.ins.player.transform.position.y, transform.position.z+0.5f);
           
            GameManager.ins.player.transform.position = pos;

            PlayerController.isOnlyLook = true;
            boxCollider.isTrigger = false;
           
            UIManager.ins.ChangeHideBushImage(true);
            StartCoroutine(EnableController());
        }
        else if(inside && !outside)
        {
            SoundManager.ins.PlayToiletOut();
            outside = true;
            inside = false;
            boxCollider.isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0, 0, 3));
            PlayerController.isOnlyLook = false;
            boxCollider.isTrigger = false;
            //isHide = false;
            UIManager.ins.ChangeHideBushImage(false);
        }
        StartCoroutine(EnableInteract());
    }
    IEnumerator EnableInteract()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerInteract.canInteractAgain = true;
    }
    IEnumerator EnableController()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.ins.player.GetComponent<CharacterController>().enabled = true;
    }

}
