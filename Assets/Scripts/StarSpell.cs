using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpell : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController player;
    public float moveSpeed = 1000f;
    public float rotationSpeed = 0f;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        
        rb = this.GetComponent<Rigidbody2D>();
        if (player.GetComponent<SpriteRenderer>().flipX)
            rb.AddForce(new Vector2(-1, 0) * moveSpeed, ForceMode2D.Impulse);
        else if (!player.GetComponent<SpriteRenderer>().flipX)
            rb.AddForce(new Vector2(1, 0) * moveSpeed, ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(rotationSpeed-15, rotationSpeed+15));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            collision.gameObject.GetComponent<Enemy>().ReduceHealth(1);
        }

        Destroy(this.gameObject);
    }
}
