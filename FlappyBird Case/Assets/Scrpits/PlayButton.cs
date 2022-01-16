using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string gameScene = "Game";

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Click()
    {
        anim.SetTrigger("Play");
        FindObjectOfType<CrossfadeLoadEffect>().ChamarCrossfade(gameScene);
        
    }
}
