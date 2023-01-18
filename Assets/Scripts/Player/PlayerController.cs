using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerHeadCam;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    HeadBob headBobScript;

    public GameObject Flashlight;
    public FlashlightAdvanced flashLightScript;

    public AudioSource walkAS;
    public AudioSource jumpAS;
    public AudioSource heartBeatAS;

    public static bool playerControl;
    public static bool isOnlyLook = false;

    private void Start()
    {
        playerControl = true;
        headBobScript = GetComponent<HeadBob>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Flashlight.SetActive(false);
        SoundManager.ins.PlayExtraAudio("PlayerWalk", walkAS);
        SoundManager.ins.PlayExtraAudio("Heartbeat", heartBeatAS);
    }

    public void UseTorch()
    {
        Flashlight.SetActive(true);
        UIManager.ins.batteryUI.SetActive(true);
    }

    void Update()
    {

        if(PlayerDeath.isAlive && playerControl)
        {

            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate move direction based on axes
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);

                if (Input.GetButton("Jump") && canMove)
                {
                    SoundManager.ins.PlayExtraAudio("PlayerJump", jumpAS);
                    moveDirection.y = jumpSpeed;
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            if(!isOnlyLook)
            {
                characterController.Move(moveDirection * Time.deltaTime);
            }
            

            // Player and Camera rotation
            if (canMove && !UIManager.ins.gameIsPaused)
            {
               
                rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
                rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);

                playerHeadCam.localRotation = Quaternion.Euler(rotation.x, 0, 0);
                transform.eulerAngles = new Vector2(0, rotation.y);
            } else
            {
                headBobScript.isWalking = false;
            }


            headBobScript.isWalking = (Mathf.Abs(characterController.velocity.x) > 0.1f || Mathf.Abs(characterController.velocity.z) > 0.1f) ? true : false;

            if (canMove && headBobScript.isWalking && characterController.isGrounded)
            {
                walkAS.UnPause();
            } else
            {
                walkAS.Pause();
            }
           
            if(Vector3.Distance(transform.position, GameManager.ins.crawler.transform.position) > 15 && Vector3.Distance(transform.position, GameManager.ins.crawler.transform.position) < 30)
            {
                heartBeatAS.UnPause();
                heartBeatAS.pitch = 0.75f;
            }
            else if(Vector3.Distance(transform.position, GameManager.ins.crawler.transform.position) < 15)
            {
                heartBeatAS.UnPause();
                heartBeatAS.pitch = 1;
            }
            else if(Vector3.Distance(transform.position, GameManager.ins.crawler.transform.position) > 30)
            {
               heartBeatAS.Pause();
            }

        }else if(!playerControl)
        {
            walkAS.Pause();
        }
    }
}
