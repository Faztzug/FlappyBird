using System;
using UnityEngine;
using UnityEngine.Events;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D birdRgbd;
    private bool dead = false;
    private bool begun = false;
    private PipeInfiniteScroll pipeInfiniteScroll;
    private Animator birdAnim;

    [SerializeField] private Vector2 force;
    [SerializeField] private float rotation;
    [SerializeField] private float gravity;

    [SerializeField] private float getReadyFlapTime;
    [SerializeField] private Vector2 getReadyFlapMove;
    private float getReadyFlapTimeCounter;
    private bool getReadyFlapUp = true;

    [SerializeField] private float contagemPosMorte;
    [SerializeField] private float contagemStart;

    [SerializeField] private float teto;

    [SerializeField] private float maximumSpeed = 2f;

    [Range(1, 4)]
    [SerializeField] private float overralSpeed;

    [SerializeField] private float overralSpeedIncrementPerPoint = 0.01f;
    [HideInInspector] private Action<float> onOverralScrollSpeed;

    [SerializeField] protected UnityEvent onDeath;
    [SerializeField] protected UnityEvent onBegun;
    [HideInInspector] public Action<int> onScorePoint;

    private SFXPlayer sfxPlayer;
    private string flapSFX;
    private string pointSFX;
    private string hitSFX;
    private string deathSFX;

    [HideInInspector] public PauseGame pause;

    private void Start()
    {
        birdAnim = GetComponent<Animator>();
        birdRgbd = GetComponent<Rigidbody2D>();
        pipeInfiniteScroll = FindObjectOfType<PipeInfiniteScroll>();
        sfxPlayer = GetComponent<SFXPlayer>();
        flapSFX = sfxPlayer.sounds[0].name;
        pointSFX = sfxPlayer.sounds[1].name;
        hitSFX = sfxPlayer.sounds[2].name;
        deathSFX = sfxPlayer.sounds[3].name;

        overralSpeed = 1f;

        foreach (InfiniteScroll infiniteScroll in FindObjectsOfType<InfiniteScroll>())
        {
            onOverralScrollSpeed += infiniteScroll.updateOverralSpeed;
        }
    }

    private void Update()
    {
        if (contagemStart > 0) contagemStart -= 1 * Time.deltaTime;

        if (dead)
        {
            if (contagemPosMorte > 0)
            {
                contagemPosMorte -= 1 * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        GetReadyMove();

        if (dead == false)
            birdRgbd.MoveRotation((rotation * birdRgbd.velocity.y) * Time.fixedDeltaTime);
    }

    private void GetReadyMove()
    {
        if (begun == false)
        {
            if (getReadyFlapTimeCounter <= getReadyFlapTime)
            {
                getReadyFlapTimeCounter += 1 * Time.fixedDeltaTime;

                if (getReadyFlapUp == true)
                    birdRgbd.MovePosition(birdRgbd.position + getReadyFlapMove * Time.fixedDeltaTime);
                else
                    birdRgbd.MovePosition(birdRgbd.position - getReadyFlapMove * Time.fixedDeltaTime);
            }
            else if (getReadyFlapTimeCounter > getReadyFlapTime)
            {
                getReadyFlapTimeCounter = 0f;
                getReadyFlapUp = !getReadyFlapUp;
            }
        }
    }

    public void Flap()
    {
        if (dead == false && contagemStart <= 0 && transform.position.y < teto)
        {
            if (begun == false)
            {
                onBegun?.Invoke();
                begun = true;
                birdRgbd.gravityScale = gravity;
            }

            Debug.Log("Flap");

            if (pipeInfiniteScroll != null)
                pipeInfiniteScroll.enabled = true;

            if (pause.isPaused == false)
                sfxPlayer.PlayAudio(flapSFX);

            birdRgbd.velocity = Vector2.zero;
            birdRgbd.AddForce(force, ForceMode2D.Impulse);
        }
        else if (dead == true && contagemPosMorte <= 0)
        {
        }
    }

    private void Death()
    {
        if (dead == false)
        {
            sfxPlayer.PlayAudio(hitSFX);
            sfxPlayer.PlayAudio(deathSFX);

            birdAnim.speed = 0;
            InfiniteScroll[] allScrolls = FindObjectsOfType<InfiniteScroll>();

            foreach (InfiniteScroll scroll in allScrolls)
            {
                scroll.enabled = false;
            }

            onDeath?.Invoke();
        }

        dead = true;
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
            if (overralSpeed < maximumSpeed)
            {
                onScorePoint?.Invoke(1);

                overralSpeed += overralSpeedIncrementPerPoint;
                onOverralScrollSpeed?.Invoke(overralSpeed);
            }
            else
            {
                onScorePoint?.Invoke(2);
            }

            sfxPlayer.PlayAudio(pointSFX);
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public bool IsPlaying()
    {
        if (dead == false && begun == true)
            return true;
        else
            return false;
    }
}