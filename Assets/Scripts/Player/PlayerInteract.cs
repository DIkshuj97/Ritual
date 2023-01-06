using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float rayRange = 4;
    [SerializeField] private Transform head;
    [SerializeField] private GameObject handIcon;
    [SerializeField] private LayerMask interactLayer;

    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = new Ray(head.position, head.forward);
        bool hit = Physics.Raycast(ray, out hitInfo, rayRange, interactLayer);
        if (hit)
        {
           // handIcon.SetActive(true);
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.GetComponent<IInteractable>() != null && Input.GetKeyDown(KeyCode.E))
            {
                hitObject.GetComponent<IInteractable>().Interact();
            }
        } else
        {
           // handIcon.SetActive(false);
        }
    }
}
