using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] allSfx;
    public Sound[] allMusic;
    public Sound[] extras;
    [SerializeField] private AudioSource EffectsSource;
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource ChaseAudioSource;
    [SerializeField] private AudioSource Hover_Bush_EquipSource;
    [SerializeField] private AudioSource Toilet_Save_Scream_AudioSource;
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
    public void PlaySave()
    {
        PlayExtraAudio("Save", Toilet_Save_Scream_AudioSource);
    }

    public void PlayToiletIn()
    {
        PlayExtraAudio("ToiletIn", Toilet_Save_Scream_AudioSource);
    }
    public void PlayToiletOut()
    {
        PlayExtraAudio("ToiletOut", Toilet_Save_Scream_AudioSource);
    }
    public void PlayChaseSound()
    {
        if(!ChaseAudioSource.isPlaying)
        {
            PlayExtraAudio("Chase", ChaseAudioSource);
        }
        
    }

    public void PlayScreamSound()
    {
        PlayExtraAudio("Scream", Toilet_Save_Scream_AudioSource);
    }

    public void PlayEquip()
    {
        PlayExtraAudio("Equip", Hover_Bush_EquipSource);
    }
    public void PlayBush()
    {
      //  hover_bushSource.ignoreListenerPause = true;
        PlayExtraAudio("Bush", Hover_Bush_EquipSource);
    }

    public void PlayHover()
    {
        Hover_Bush_EquipSource.ignoreListenerPause = true;
        PlayExtraAudio("Click", Hover_Bush_EquipSource);
    }
    public void PlayPress()
    {
        Hover_Bush_EquipSource.ignoreListenerPause = true;
        PlayExtraAudio("Press", Hover_Bush_EquipSource);
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
    void SetMusicandSfx(Sound sound, AudioSource _aS)
    {
        sound.source = _aS;
        _aS.clip = sound.clip;
        _aS.volume = sound.volume;
        _aS.pitch = sound.pitch;
        _aS.loop = sound.loop;
    }
    
    public void StopChase()
    {
        ChaseAudioSource.Stop();
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
        SetMusicandSfx(snd, EffectsSource);
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
        SetMusicandSfx(snd, MusicSource);
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
