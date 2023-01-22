using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
   [HideInInspector] public Animator anim;
    bool TriggerOnce;

    public static bool isAlive;

    AudioSource aS;
    AudioSource aS_2;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        anim.enabled = false;
        TriggerOnce = true;
        isAlive = true;
        aS = GetComponent<PlayerController>().jumpAS; // for water splash sound
        aS_2 = GetComponent<PlayerController>().walkAS; // for death sound
    }

    private void Update()
    {

     //  if (Input.GetKeyDown(KeyCode.G)) TriggerDeath();

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {       
        if (hit.gameObject.layer ==4 && TriggerOnce) //4 : water layer
        {
            TriggerOnce = false;
            SoundManager.ins.PlayExtraAudio("WaterJump", aS);
            TriggerDeath();
        }
    }

    public void TriggerDeath()
    {
        SoundManager.ins.PlayExtraAudio("PlayerDeath", aS);
        isAlive = false;
        anim.enabled = true;
        anim.SetTrigger("Death");
        UIManager.ins.bloodScreen.SetActive(true);
        StartCoroutine(ShowGameOverMenu());
    }

    public void RespawnAnim()
    {
        TriggerOnce = true;
        anim.SetTrigger("Respawn");
    }
    public void DisableAnimator()
    {
        isAlive = true;
        anim.enabled = false;
    }
    IEnumerator ShowGameOverMenu()
    {
        yield return new WaitForSeconds(2);
        UIManager.ins.GameOverUI(true);
    }
}
