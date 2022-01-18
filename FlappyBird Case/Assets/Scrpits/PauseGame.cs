using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private float unpauseWait;
    private BirdController bird;
    [HideInInspector] public bool isPaused = false;
    private Animator anim;

    public void Start()
    {
        bird = FindObjectOfType<BirdController>();
        bird.pause = this;
        anim = GetComponentInChildren<Animator>();
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
}
