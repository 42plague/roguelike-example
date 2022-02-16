using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;

    public LayerMask whatIsSolid;
    public GameObject hitEffect;

    private void Start()
    {
        Invoke("destroyBullet", lifetime);
    }
    void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Skeleton>().TakeDamage(damage);
            }
            destroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void destroyBullet()
    {
        Instantiate(hitEffect, transform.position = new
        Vector3(transform.position.x, transform.position.y, -3f),
        Quaternion.identity);
        Destroy(gameObject);
    }
}