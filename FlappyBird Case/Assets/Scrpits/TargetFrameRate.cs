using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    [Range(30,60)]
    [SerializeField] private int FPS = 30;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPS;
    }

    
}
