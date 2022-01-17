using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private string playThis;
    private string CurrentPlaying;
    //[Range(0,1)]
    //public float overallVolume = 1;
    private AudioSource[] allAudioSources;


    private void Start()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;

        }

        PlayAudio(playThis);

        allAudioSources = GetComponents<AudioSource>();

        
    }

    public void ChangeMusic(string name)
    {
        if (CurrentPlaying == null)
            PlayAudio(name);

        if(CurrentPlaying != name && CurrentPlaying != null)
        {
            foreach (Sound sound in sounds)
            {
                sound.audioSource.Stop();
                
            }

            Debug.Log("Musica: " + CurrentPlaying + "alterada para: " + name);
            PlayAudio(name);
        }
    }

    public void PlayAudio(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.audioSource.Play();
                CurrentPlaying = sound.name;
            }
                
        }
    }

    

}
