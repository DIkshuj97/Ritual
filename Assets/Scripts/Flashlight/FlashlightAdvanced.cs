using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashlightAdvanced : MonoBehaviour
{
    Light f_light;

    [SerializeField] private float lifetime;
    [SerializeField] private float batteries;

    bool on;
    bool off;

   [SerializeField] private float flickerSpeed;
   [SerializeField] private float flickerNoise;

    public bool canFlicker = false;
    float flickLimit;

    public float baseIntensity = 1f;
    public float intensityVariance;

    float initialIntensity;

   // public bool stopFlickering = false;
    void Start()
    {
        f_light = GetComponent<Light>();
        flickLimit = lifetime / 100 * 20;
        initialIntensity = f_light.intensity;
        off = true;
        f_light.enabled = false;
      //  StartCoroutine(Flicker());
    }

    private void TurnOn()
    {
        // sound of button on
        f_light.enabled = true;
        on = true;
        off = false;
    }

    private void TurnOff()
    {
        // sound of button off
        f_light.enabled = false;
        on = false;
        off = true;
    }
    void Update()
    {
        Debug.Log(f_light.intensity);
        if (lifetime < flickLimit && lifetime > 0)
        {
            canFlicker = true;
        }
        if (canFlicker) FlickerUsingPerlinNoise();

        if (Input.GetKeyDown(KeyCode.F) && off)
        {
            TurnOn();
        }
        else if (Input.GetKeyDown(KeyCode.F) && on)
        {
            TurnOff();
        }

        if (on && lifetime > 0) lifetime -= 1 * Time.deltaTime;

        if (lifetime <= 0)
        {
            f_light.intensity = initialIntensity;
            canFlicker = false;
            f_light.enabled = false;
            on = false;
            off = true;
            lifetime = 0;
        }

        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        if (Input.GetKeyDown(KeyCode.R) && batteries >= 1)
        {
            if (batteries <= 0)
            {
                batteries = 0;
                return;
            }
            batteries -= 1;
            lifetime += 10;
            flickLimit = lifetime / 100 * 20;

        }

        // Debug.Log(canFlicker);
        //  if (Input.GetKeyDown(KeyCode.R) && batteries == 0)
        //  {
        //      return;
        //    }
    }

  
    private void FlickerUsingPerlinNoise()
    {
        float intensity = baseIntensity + (intensityVariance * baseIntensity) * Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);

        // avoid going into negative intensity 
        if (intensity <= 0)
            intensity = baseIntensity * 0.01f;

        f_light.intensity = intensity;
    }

}
