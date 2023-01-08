using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Objective : MonoBehaviour
{
   [TextArea(3,10)] [SerializeField] private string objectiveText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ObjectiveManager.ins.SetText(objectiveText);
            ObjectiveManager.ins.PlayAnim(gameObject);
        }
    }
}
