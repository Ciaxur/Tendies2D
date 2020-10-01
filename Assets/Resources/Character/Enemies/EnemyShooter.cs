using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    // External References
    public GameObject firePoint;
    public GameObject bullet;
    public float bulletSpeed = 10f;


    public void shootAt(Vector3 target) {
        // Rotate in Direction of Target
        Vector2 lookDirection = target - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        GameObject obj = Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(0f, 0f, angle));
        obj.GetComponent<EntityDeath>().setRefObject(gameObject);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(lookDirection.normalized * bulletSpeed, ForceMode2D.Impulse);

        // Ignore Shooter of Bullet
        Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
