using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Transform references")]
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform cameraTransform;

    [Header("Head bobbing")]
    [SerializeField] private float bobFrequency = 5f;
    [SerializeField] private float bobHorizonalAmplitude = 0.1f;
    [SerializeField] private float bobverticalAmplitude = 0.1f;
   [Range(0, 1)] [SerializeField] private float headBobSmoothing = 0.1f;
    // State
    [HideInInspector] public bool isWalking;
    private float walkingTime;
    private Vector3 targetCameraPosition;
    private void Update()
    {
        if(PlayerDeath.isAlive && PlayerController.playerControl)
        {
            // Set time and offset to 0
            if (!isWalking) walkingTime = 0;
            else walkingTime += Time.deltaTime;
       
            // Calculate the camera's target position
            targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);
        
            // Interpolate positon
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);
            // Snap to position 1f it is close enough
            if((cameraTransform.position - targetCameraPosition).magnitude < -0.001) cameraTransform.position = targetCameraPosition;
        }
    }


    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontaloffset = 0;
        float verticaloffset = 0;
        Vector3 offset = Vector3.zero;
        if (t > 0)
        {
            // Calculate offsets
            horizontaloffset = Mathf.Cos(t * bobFrequency) * bobHorizonalAmplitude;
            verticaloffset = Mathf.Sin(t * bobFrequency * 2) * bobverticalAmplitude;
            // Combine offsets relat ive to the head's position and calculate the camera's target position
            offset = headTransform.right * horizontaloffset + headTransform.up * verticaloffset;
        }
        return offset;
        }
    }
