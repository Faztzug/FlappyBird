using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInfiniteScroll : InfiniteScroll
{
    [SerializeField] private Vector2 randomHeight;


    

    private void Start()
    {
        onScrollPartRecycle += OnPartRecycled;

        foreach (Transform part in parts)
        {
            
            DoRandomHeight(part);
        }
    }

    void DoRandomHeight(Transform part)
    {
        if (part != null)
        {
            float rng = Random.Range(randomHeight.x, randomHeight.y);
            part.position = new Vector2(part.position.x, rng);
        }
            
    }

    void OnPartRecycled(Transform part)
    {
        DoRandomHeight(part);
        part.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    
}
