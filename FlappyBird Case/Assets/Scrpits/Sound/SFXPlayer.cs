using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public Sound[] sounds;
    
    [Range(0, 1)]
    public float overallVolume = 1;
    private AudioSource[] allAudioSources;

    private void Awake()
    {
        

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop = false;

        }
    }


    private void Start()
    {

        allAudioSources = GetComponents<AudioSource>();

        UpdateVolume(overallVolume);
    }

    

    public void PlayAudio(string name)
    {
        
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.audioSource.Play();
                Debug.Log("Play: " + name);
            }
            
        }
    }

    public void StopAudio(string name)
    {

        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.audioSource.Stop();
                
            }

        }
    }

    public void StopAll()
    {
        foreach (AudioSource sfx in GetComponents<AudioSource>())
        {
            sfx.Stop();
        }
    }

    public void UpdateVolume(float volume)
    {
        overallVolume = volume;
        StartCoroutine(UpdateVolumeCourotine());
    }

    IEnumerator UpdateVolumeCourotine()
    {
        yield return new WaitForEndOfFrame();

        int length = allAudioSources.Length;
        Debug.Log("SFX Update Volume");

        for (int i = 0; i < length; i++)
        {
            allAudioSources[i].volume = sounds[i].volume * overallVolume;
        }

        
    }
}
