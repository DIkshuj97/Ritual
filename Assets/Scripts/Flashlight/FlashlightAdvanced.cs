using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashlightAdvanced : MonoBehaviour
{
    Light f_light;

    [SerializeField] private float lifetime;
    float lifeAmount;
    public float batteries; // Extra batteries

    bool on;
    bool off;

    [SerializeField] private float flickerSpeed;
    [SerializeField] private float flickerNoise;

    [SerializeField] private bool canFlicker = false;
    float flickLimit;
    [SerializeField] private int flickPercent;

    [SerializeField] private float baseIntensity = 1f;
    [SerializeField] private float intensityVariance;

    float initialIntensity;
   [HideInInspector] public float fillvalue;

    [SerializeField] private AudioSource aS;
    void Start()
    {
        f_light = GetComponent<Light>();
        lifeAmount = lifetime;
        flickLimit = lifetime / 100 * flickPercent;
        initialIntensity = f_light.intensity;
        off = true;
        f_light.enabled = false;
    }

    private void TurnOn()
    {
        // sound of button on
        SoundManager.ins.PlayExtraAudio("FlashlightToggle", aS);
        f_light.enabled = true;
        on = true;
        off = false;
    }

    private void TurnOff()
    {
        // sound of button off
        SoundManager.ins.PlayExtraAudio("FlashlightToggle", aS);
        f_light.enabled = false;
        on = false;
        off = true;
    }
    void Update()
    {
       

        if (lifetime < flickLimit && lifetime > 0)
        {
            canFlicker = true;
        }
        if (canFlicker) Flicker();

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
            batteries -= 1;
            lifetime += lifeAmount;
            flickLimit = lifetime / 100 * flickPercent;
        }

        fillvalue = lifetime / lifeAmount;
    }

    public void GotBattery()
    {
        if (batteries >= 3) return;
        batteries++;
    }
  
    private void Flicker()
    {
        float intensity = baseIntensity + (intensityVariance * baseIntensity) * Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);

        // avoid going into negative intensity 
        if (intensity <= 0)
            intensity = baseIntensity * 0.01f;

        f_light.intensity = intensity;
    }

}
