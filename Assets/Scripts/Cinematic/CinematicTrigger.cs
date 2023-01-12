using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    bool alreadyTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered && other.gameObject.tag == "Player")
        {
            Debug.Log("Play");
            GetComponent<PlayableDirector>().Play();
            alreadyTriggered = true;
        }
    }
}
