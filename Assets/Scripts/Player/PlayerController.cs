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

    public AudioSource walkaS;
    public AudioSource jumpaS;

    void Start()
    {
        GameManager.ins.player = gameObject;
        headBobScript = GetComponent<HeadBob>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Flashlight.SetActive(false);
        SoundManager.ins.PlayExtraAudio("PlayerWalk", walkaS);
    }

    void Update()
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
                SoundManager.ins.PlayExtraAudio("PlayerJump", jumpaS);
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

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
            walkaS.UnPause();
        } else
        {
            walkaS.Pause();
        }
    }
}
