using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Head : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;

    public float startTimeBtwShots;

    private float timeBtwShots;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 direction =
        Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetBool("Mouse_Click", true);
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                animator.SetBool("Mouse_Click", false);
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}