using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 15.0f;
    float angle;
    Vector2 direction;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        angle = Mathf.Deg2Rad * transform.eulerAngles.z;
        direction = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
        Vector2 velocity = new Vector2(direction.x, direction.y) * (bulletSpeed);
        rigidbody2d.velocity = velocity;
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Asteroids")) Destroy(gameObject);
    }
}
