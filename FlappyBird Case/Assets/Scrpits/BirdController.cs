using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class BirdController : MonoBehaviour
{

    private Rigidbody2D birdRgbd;
    private bool dead;
    private PipeInfiniteScroll pipeInfiniteScroll;

    [SerializeField] private Vector2 force;
    [SerializeField] private float rotation;

    [SerializeField] private float contagemPosMorte;
    [SerializeField] private float contagemStart;
    
    [SerializeField] private float teto;
    [SerializeField] private string gameScene = "Game";

    [SerializeField] protected UnityEvent onDeath;
    [HideInInspector] public Action <int> onScorePoint;



    
    void Start()
    {
        
        birdRgbd = GetComponent<Rigidbody2D>();
        pipeInfiniteScroll = FindObjectOfType<PipeInfiniteScroll>();
    }

    
    void Update()
    {
        if (contagemStart > 0) contagemStart -= 1 * Time.deltaTime;

        if (dead)
        {
            if(contagemPosMorte > 0)
            {
                contagemPosMorte -= 1 * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if(dead == false)
        birdRgbd.MoveRotation((rotation * birdRgbd.velocity.y) * Time.fixedDeltaTime);
    }

    public void Flap()
    {
        if(dead == false && contagemStart <= 0 && transform.position.y < teto)
        {
            Debug.Log("Flap");

            if(pipeInfiniteScroll != null)
            pipeInfiniteScroll.enabled = true;

            birdRgbd.velocity = Vector2.zero;
            birdRgbd.AddForce(force, ForceMode2D.Impulse);
        } else if (dead == true && contagemPosMorte <= 0)
        {
            
                SceneManager.LoadScene(gameScene);
            
        }
        

    }

    private void Death()
    {
        dead = true;
        InfiniteScroll[] allScrolls = FindObjectsOfType<InfiniteScroll>();

        foreach (InfiniteScroll scroll in allScrolls)
        {
            scroll.enabled = false;
        }

        onDeath?.Invoke();

    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            
            onScorePoint?.Invoke(1);
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
            
    }
}
