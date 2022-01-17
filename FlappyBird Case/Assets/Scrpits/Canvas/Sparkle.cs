using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    [SerializeField] private Vector2 waitTimeRange;
    [SerializeField] private Vector2 positionTranslateRange;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;

        transform.Translate(new Vector2(
            Random.Range(positionTranslateRange.x, positionTranslateRange.y),
            Random.Range(positionTranslateRange.x, positionTranslateRange.y))
            );

        StartCoroutine(waitToStart(Random.Range(waitTimeRange.x, waitTimeRange.y)));
    }

    IEnumerator waitToStart(float time)
    {
        yield return new WaitForSeconds(time);

        anim.speed = 1;
    }

    
}
