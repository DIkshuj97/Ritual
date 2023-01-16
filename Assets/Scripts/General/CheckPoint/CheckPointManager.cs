using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
   
   

   // private Animator thisAnimator;

    private List<GameObject> CheckPointsList;
    private Vector3 pos;
    public Vector3 GetActiveCheckPointPosition()
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = pos;

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPoint>().Activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }
        return result;
    }
    public void ActivateCheckPoint(CheckPoint activeCheckpoint)
    {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPoint>().Activated = false;
            // cp.GetComponent<Animator>().SetBool("Active", false);
        }
        // We activated the current checkpoint
        activeCheckpoint.Activated = true;
        //thisAnimator.SetBool("Active", true);
        StartCoroutine(UIManager.ins.ShowSaveUI());
    }

    void Start()
    {
        pos = GameManager.ins.player.transform.position;
       // thisAnimator = GetComponent<Animator>();

        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint").ToList();
    }

}