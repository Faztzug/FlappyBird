using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private float unpauseWait;
    private BirdController bird;
    [HideInInspector] public bool isPaused = false;
    private Animator anim;

    private Scrollbar musicScroll;
    private Scrollbar sfxScroll;
    private SFXPlayer sfxPlayer;
    private MusicPlayer musicPlayer;

    [SerializeField] private GameObject pauseMenu;

    public void Start()
    {
        bird = FindObjectOfType<BirdController>();
        bird.pause = this;
        anim = GetComponentInChildren<Animator>();

        Scrollbar[] scrolls = GetComponentsInChildren<Scrollbar>();

        musicScroll = scrolls[0];
        sfxScroll = scrolls[1];

        sfxPlayer = FindObjectOfType<SFXPlayer>();
        musicPlayer = FindObjectOfType<MusicPlayer>();

        LoadSettings();

        pauseMenu.SetActive(false);

    }

    public void Pause()
    {
        if(bird.IsPlaying())
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Unpause()
    {
        if (bird.IsPlaying())
            StartCoroutine(UnpauseCourotine());
        else
            isPaused = false;
    }

    IEnumerator UnpauseCourotine()
    {
        anim.SetTrigger("Unpause");

        yield return new WaitForSecondsRealtime(unpauseWait);

        Time.timeScale = 1f;

        isPaused = false;
    }

    public void UpdateVolume()
    {       
        sfxPlayer.UpdateVolume(sfxScroll.value);
        musicPlayer.UpdateVolume(musicScroll.value);
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxScroll.value);
        PlayerPrefs.SetFloat("MusicVolume", musicScroll.value);
    }

    private void LoadSettings()
    {
        sfxScroll.value = PlayerPrefs.GetFloat("SFXVolume");
        musicScroll.value = PlayerPrefs.GetFloat("MusicVolume");
        UpdateVolume();
    }
}
