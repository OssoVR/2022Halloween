using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    int health = 3;
    public Sprite birdDefault;
    public Sprite birdSpell;
    public GameObject starPrefab;
    public Transform starSpawnPosL;
    public Transform starSpawnPosR;
    float cooldown = 0f;
    public float maxCooldown = 0.2f;
    float jumpForce = 100f;
    SpriteRenderer r;
    BirbManager mgr;
    Rigidbody2D rb;

    [Space(5)]
    public GameObject gameOverScreen;
    public Sprite hpFull;
    public Sprite hpEmpty;
    public Image hp1;
    public Image hp2;
    public Image hp3;
    
    // Start is called before the first frame update
    void Start()
    {
        mgr = FindObjectOfType<BirbManager>();
        r = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        r.sprite = birdDefault;

        hp1.sprite = hpFull;
        hp2.sprite = hpFull;
        hp3.sprite = hpFull;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                StartCoroutine(CastSpell(1));
            if (Input.GetKeyDown(KeyCode.RightArrow))
                StartCoroutine(CastSpell(0));
        }

        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        CheckHealth();  
    }

    IEnumerator CastSpell(int dir)
    {
        this.GetComponent<AudioSource>().Play();

        if (dir == 1)
        {
            cooldown = maxCooldown;
            r.flipX = true;
            r.sprite = birdSpell;
            Instantiate(starPrefab, starSpawnPosL.position, Quaternion.identity);
            rb.AddForce((transform.up + transform.right) * jumpForce);
            yield return new WaitForSeconds(maxCooldown);
            r.sprite = birdDefault;
        }

        if (dir == 0)
        {
            cooldown = maxCooldown;
            r.flipX = false;
            r.sprite = birdSpell;
            Instantiate(starPrefab, starSpawnPosR.position, Quaternion.identity);
            rb.AddForce((transform.up + -transform.right) * jumpForce);
            yield return new WaitForSeconds(maxCooldown);
            r.sprite = birdDefault;
        }
    }

    public void ReduceHealth(int hp)
    {
        health -= hp;
    }

    void CheckHealth()
    {
        switch (health)
        {
            case 3:
                hp1.sprite = hpFull;
                hp2.sprite = hpFull;
                hp3.sprite = hpFull;
                break;
            case 2:
                hp1.sprite = hpFull;
                hp2.sprite = hpFull;
                hp3.sprite = hpEmpty;
                break;
            case 1:
                hp1.sprite = hpFull;
                hp2.sprite = hpEmpty;
                hp3.sprite = hpEmpty;
                break;
            case 0:
                hp1.sprite = hpEmpty;
                hp2.sprite = hpEmpty;
                hp3.sprite = hpEmpty;
                break;
        }

        if (health <= 0)
            StartCoroutine(StartDeath());
    }

    IEnumerator StartDeath()
    {
        this.transform.Rotate(0, 15f, 0);
        Destroy(this.gameObject.GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(0.8f);
        ShowGameOver();
        Destroy(this.gameObject);
    }

    void ShowGameOver()
    {
        mgr.EndGame();
    }
}
