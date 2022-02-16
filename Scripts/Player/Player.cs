using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int hp;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Transform Body;
    public GameObject deathEffect;
    public GameObject GOscreen;

    private Rigidbody2D rb2D;
    private float h;
    private float v;
    private float startTimeBtwDmg = 1f;
    private float timeBtwDmg;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Movement
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        rb2D.velocity = new Vector2(h, v).normalized * speed * 5;
        //Body
        {
            Animator bodyAnimator = Body.GetComponent<Animator>();

            if (new Vector2(h, v) != new Vector2(0, 0))
            {
                bodyAnimator.SetBool("PlayerMoving", true);
            }
            else
            {
                bodyAnimator.SetBool("PlayerMoving", false);
            }
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, new Vector2(h, v));
            Body.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10000 * Time.deltaTime);
        }
        timeBtwDmg -= Time.deltaTime;

        if (hp > numOfHearts)
        {
            hp = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (timeBtwDmg <= 0)
        {
            hp -= damage;

            if (hp <= 0)
            {
                hearts[0].sprite = emptyHeart;
                Instantiate(deathEffect, transform.position = 
                    new Vector3(transform.position.x, transform.position.y, -3f), Quaternion.identity);
                DestroyPlayer();
            }
            timeBtwDmg = startTimeBtwDmg;
        }
    }

    public void restoreHp(int health)
    {
        hp += health;
    }

    public void DestroyPlayer()
    {
        GOscreen.SetActive(true);
        Destroy(gameObject);
    }
}