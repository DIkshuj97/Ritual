using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) TriggerDeath();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {       
        if (hit.gameObject.layer ==4) //4 : water layer
        {
            TriggerDeath();
        }
    }

    public void TriggerDeath()
    {
        anim.enabled = true;
        anim.SetTrigger("Death");
        UIManager.ins.bloodScreen.SetActive(true);
        StartCoroutine(ShowGameOverMenu());
    }

    IEnumerator ShowGameOverMenu()
    {
        yield return new WaitForSeconds(2);
        UIManager.ins.gameOverMenu.SetActive(true);
    }
}
