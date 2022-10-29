using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float moveSpeed;
    public int scoreValue;
    PlayerController player;
    BirbManager mgr;
    SpriteRenderer r;
    Rigidbody2D rb;
    AudioSource a;
    //public Animation anim;
    bool isDying = false;
    public bool isBat = false;
    public bool isGhost = false;
    public bool isPumpkin = false;
    public bool isSkull = false;

    bool isMovingToRight;

    float batTimer = 1f;
    bool startGhostTransparency = false;
    float ghostTransparency = 1f;
    float ghostTimer = 1.5f;
    float skullTimer = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mgr = FindObjectOfType<BirbManager>();
        r = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        a = GetComponent<AudioSource>();
        Physics2D.IgnoreLayerCollision(8, 8);

        if (player.transform.position.x - this.transform.position.x > 0)
        {
            isMovingToRight = false;
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            isMovingToRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (player != null)
            this.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        AnimateEnemy();
        
        if (health <= 0)
            StartCoroutine(StartDeath());
    }

    public void ReduceHealth(int hp)
    {
        health -= hp;
        if (health <= 0)
        {
            isDying = true;
            a.Play();
        }
    }

    IEnumerator StartDeath()
    {
        Destroy(this.gameObject.GetComponent<CircleCollider2D>());
        mgr.IncreaseScore(scoreValue);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            collision.gameObject.GetComponent<PlayerController>().ReduceHealth(1);
            Destroy(this.gameObject);
        }
    }

    void AnimateEnemy()
    {
        if (isDying)
        {
            this.transform.Rotate(0, 15f, 0);
            this.transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime, transform.localScale.z - Time.deltaTime);
            Physics2D.IgnoreLayerCollision(11, 10, false);
        }
        
        if (isBat)
        {
            batTimer -= Time.deltaTime;

            if (batTimer <= 0)
            {
                rb.AddForce(this.transform.up * Random.Range(5f, 10f), ForceMode2D.Impulse);
                batTimer = Random.Range(1f, 2f);
            }
        }

        if (isGhost)
        {
            if (startGhostTransparency)
            {
                ghostTimer += Time.deltaTime;
                ghostTransparency = ghostTimer;
                if (ghostTimer >= 1)
                {
                    startGhostTransparency = false;
                    ghostTransparency = 1;
                }
            }
            if (!startGhostTransparency)
            {
                ghostTimer -= Time.deltaTime;
                ghostTransparency = ghostTimer;
                if (ghostTimer <= 0)
                {
                    startGhostTransparency = true;
                    ghostTransparency = 0;
                }
            }
            
            r.color = new Color(1, 1, 1, ghostTransparency);
            
            if (r.color.a >= 0.3f)
                Physics2D.IgnoreLayerCollision(11, 10, false);
            else
                Physics2D.IgnoreLayerCollision(11, 10, true);
        }

        if (isPumpkin)
        {
            if (isMovingToRight)
                this.transform.Rotate(0, 0, 1);
            else
                this.transform.Rotate(0, 0, -1);
        }

        if (isSkull)
        {
            skullTimer -= Time.deltaTime;

            if (skullTimer <= 0)
            {
                rb.AddForce(this.transform.up * Random.Range(1f, 2f), ForceMode2D.Impulse);
                batTimer = Random.Range(3f, 5f);
            }
        }
    }
}
