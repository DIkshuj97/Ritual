using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class CinematicTrigger : MonoBehaviour
{
    bool alreadyTriggered = false;
    public string clipName;



    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered && other.gameObject.tag == "Player")
        {
            SoundManager.ins.PlaySfx(clipName);
           
            GetComponent<PlayableDirector>().Play();
           
            alreadyTriggered = true;
        }
    }

    public void TriggerCutscene()
    {
        if (!alreadyTriggered)
        {
            Debug.Log("Play");
            GetComponent<PlayableDirector>().Play();
            alreadyTriggered = true;
        }
    }
}
