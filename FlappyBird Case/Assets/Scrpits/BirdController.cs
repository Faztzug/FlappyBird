using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{

    private Rigidbody2D birdRgbd;
    private bool dead;
    [SerializeField] private Vector2 force;
    private PipeInfiniteScroll pipeInfiniteScroll;
    [SerializeField] private Text score;
    [SerializeField] private float contagemPosMorte;
    [SerializeField] private float contagemStart;
    private int currentScore;

    [SerializeField] protected UnityEvent onDeath;


    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        birdRgbd = GetComponent<Rigidbody2D>();
        pipeInfiniteScroll = FindObjectOfType<PipeInfiniteScroll>();
    }

    // Update is called once per frame
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

    public void Flap()
    {
        if(dead == false && contagemStart <= 0)
        {
            Debug.Log("Flap");
            pipeInfiniteScroll.enabled = true;

            birdRgbd.velocity = Vector2.zero;
            birdRgbd.AddForce(force, ForceMode2D.Impulse);
        } else if (dead == true && contagemPosMorte <= 0)
        {
            
                SceneManager.LoadScene(0);
            
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

    private void Score()
    {
        currentScore++;
        score.text = currentScore.ToString();
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
            Score();
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
            
    }
}
