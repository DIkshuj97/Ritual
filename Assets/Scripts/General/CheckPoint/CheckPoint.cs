using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public bool Activated = false;
    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.CompareTag("Player"))
        {
            SoundManager.ins.PlaySave();
            GameManager.ins.checkpointManager.ActivateCheckPoint(this);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
