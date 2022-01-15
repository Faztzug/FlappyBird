using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;
    [SerializeField] protected Transform[] parts;
    [SerializeField] private float partWidth;
    private int partsLength;
    protected Action <Transform> onScrollPartRecycle;
    
    

    private void Awake()
    {
        int childNumber = transform.childCount;
        parts = new Transform[childNumber];

        partsLength = parts.Length;

        for (int i = 0; i < childNumber; i++)
        {
            parts[i] = transform.GetChild(i).transform;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform part in parts)
        {
            if (part != null)
            {
                part.Translate(scrollSpeed * Time.deltaTime);

                if (part.position.x < partWidth * partsLength / 2 * -1)
                {
                    part.Translate(new Vector2(partWidth * partsLength, 0));

                    
                    onScrollPartRecycle?.Invoke(part);
                }
            }
            
        }
    }
}
