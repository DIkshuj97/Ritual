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
            GameManager.ins.player.GetComponent<CharacterController>().enabled = false;
            boxCollider.isTrigger = true;
            Vector3 pos = new Vector3(transform.position.x, GameManager.ins.player.transform.position.y, transform.position.z+0.5f);
           
            GameManager.ins.player.transform.position = pos;

            PlayerController.isOnlyLook = true;
            boxCollider.isTrigger = false;
            isHide = true;
            UIManager.ins.ChangeHideBushImage(true);
            StartCoroutine(EnableController());
        }
        else
        {
            boxCollider.isTrigger = true;
            GameManager.ins.player.GetComponent<CharacterController>().Move(new Vector3(0, 0, 3));
            PlayerController.isOnlyLook = false;
            boxCollider.isTrigger = false;
            isHide = false;
            UIManager.ins.ChangeHideBushImage(false);
        }
    }

    IEnumerator EnableController()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.ins.player.GetComponent<CharacterController>().enabled = true;
    }

}
