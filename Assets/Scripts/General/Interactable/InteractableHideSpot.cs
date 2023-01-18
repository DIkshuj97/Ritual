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
            Debug.Log("interact show");
            //move player
            GameManager.ins.player.GetComponent<CharacterController>().enabled = false;
           
            GameManager.ins.player.transform.position = transform.position;
            GameManager.ins.player.GetComponent<CharacterController>().enabled = true;
            //GameManager.ins.player.GetComponent<PlayerController>().canMove = false;
            //disable control
            isHide = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            if(isHide)
            {
                Debug.Log("interact Hide");
                GameManager.ins.player.GetComponent<CharacterController>().enabled = false;

                Vector3 playerPos = transform.position + new Vector3(0, 0, 10);
                GameManager.ins.player.transform.position = playerPos;
                GameManager.ins.player.GetComponent<CharacterController>().enabled = true;
      
                isHide = false;
            }
        }
    }

}
