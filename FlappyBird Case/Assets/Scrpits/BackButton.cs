using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Click()
    {
        anim.SetTrigger("Play");
        Application.Quit();

    }
}
