using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public int hp;
    public float speed;
    public int damage = 1;

    public GameObject particles;
    public GameObject sprite;

    private Rigidbody2D rb2D;
    private Player player;
    private Animator animator;
    private EnemySpawner spawn;

    private void Start()
    {
        animator = sprite.GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        spawn = GetComponentInParent<EnemySpawner>();
    }

    void FixedUpdate()
    {
        if (hp == 0)
        {
            spawn.enemies.Remove(gameObject);
            Destroy(gameObject);
            Instantiate(particles, transform.position = 
                new Vector3(transform.position.x, transform.position.y, -3f), Quaternion.identity);
        }

        if (player != null)
        {
            Vector2 vectorToPlayer = player.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector2.right, vectorToPlayer);
            rb2D.velocity = transform.right * speed;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            rb2D.velocity = transform.right * 0;
            animator.SetBool("IsMoving", false);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.takeDamage(damage);
        }
    }
}