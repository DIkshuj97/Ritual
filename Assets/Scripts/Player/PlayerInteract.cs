using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float rayRange = 4;
    [SerializeField] private Transform head;
    [SerializeField] private LayerMask interactLayer;

    public static bool canInteractAgain = true;
    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = new Ray(head.position, head.forward);
        bool hit = Physics.Raycast(ray, out hitInfo, rayRange, interactLayer);
        if (hit && PlayerController.playerControl)
        {
            UIManager.ins.handIcon.SetActive(true);
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.GetComponent<IInteractable>() != null && Input.GetKeyDown(KeyCode.E)  && canInteractAgain)
            {
                hitObject.GetComponent<IInteractable>().Interact();
                canInteractAgain = false;
            }
        }
        else
        {
            UIManager.ins.handIcon.SetActive(false);
        }
    }
}

   
    
