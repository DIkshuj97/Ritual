using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHideSpot : MonoBehaviour, IInteractable
{
    [SerializeField] BoxCollider boxCollider;

    public void Interact()
    {
        if (HideManager.outside  && !HideManager.inside)
        {
            GameManager.ins.dLight.SetActive(true);
            SoundManager.ins.PlayToiletIn();
            HideManager.HideToggle(true);
            GameManager.ins.player.GetComponent<CharacterController>().enabled = false;
            boxCollider.isTrigger = true;
            Vector3 pos = new Vector3(transform.position.x, GameManager.ins.player.transform.position.y, transform.position.z);

            GameManager.ins.player.transform.position = pos;

            PlayerController.isOnlyLook = true;
            boxCollider.isTrigger = false;

            UIManager.ins.ChangeHideBushImage(true);
            StartCoroutine(EnableController());
        }
        else if (HideManager.inside && !HideManager.outside)
        {
            GameManager.ins.dLight.SetActive(false);
            SoundManager.ins.PlayToiletOut();
            HideManager.HideToggle(false);
            boxCollider.isTrigger = true;
            Vector3 dir = GameManager.ins.player.transform.TransformDirection(Vector3.forward * 4);
            GameManager.ins.player.GetComponent<CharacterController>().Move(dir);
            PlayerController.isOnlyLook = false;
            UIManager.ins.ChangeHideBushImage(false);
            StartCoroutine(DisableTrigger());
        }
        StartCoroutine(EnableInteract());
    }

    IEnumerator DisableTrigger()
    {
        yield return new WaitForSeconds(0.2f);
        boxCollider.isTrigger = false;
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
