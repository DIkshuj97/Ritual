using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] allSfx;
    public Sound[] allMusic;
    public Sound[] extras;
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    [SerializeField] private AudioSource hoverSource;
    public static SoundManager ins;
    void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
        // DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SetAllSfx();
        SetAllMusic();
        PlayMusic("BGM");
    }
    public void PlayHover()
    {
        hoverSource.ignoreListenerPause = true;
        PlayExtraAudio("Click", hoverSource);
    }
    void SetAllSfx()
    {
        foreach (Sound sound in allSfx)
        {
            sound.source = EffectsSource; //gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }
    void SetAllMusic()
    {
        foreach (Sound sound in allMusic)
        {
            sound.source = MusicSource; //gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void SetExtras(Sound sound,AudioSource _aS)
    {
      
            sound.source = _aS;
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
     
    }
    public void StopSfx()
    {
        EffectsSource.Stop();
    }
    public void StopMusic()
    {
        MusicSource.Stop();
    }
    
    public void SetAudioClip(string name, AudioSource aSource)
    {
        Sound snd = Array.Find(extras, sound => sound.name == name);
        aSource.clip = snd.clip;
    }
    public void PlayExtraAudio(string name,AudioSource aSource)
    {
        Sound snd = Array.Find(extras, sound => sound.name == name);
        SetExtras(snd,aSource);
        try
        {
            snd.source.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning("extra clip not found");
        }
    }
    public void PlaySfx(string name)
    {
        Sound snd = Array.Find(allSfx, sound => sound.name == name);
        try
        {
            snd.source.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning("sfx not found");
        }
    }
    public void PlayMusic(string name)
    {
        Sound snd = Array.Find(allMusic, sound => sound.name == name);
        try
        {
            snd.source.Play();
        }
        catch (Exception e)
        {
            Debug.LogWarning("music not found");
        }
    }
}
