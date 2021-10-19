using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 direction;
    public static bool  isDead = false;
    float angle;
    public float bulletTimer;
    public float ShootInterval;
    public float speed;
    public float scale = 50.0f;
    public float rotationSpeed;
    Rigidbody2D rigidbody2d;
    private float vertical;

    public GameObject bullet;
    public Animator animator;
    [SerializeField] private ParticleSystem Propulsion;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        angle = Mathf.Deg2Rad * transform.eulerAngles.z;
        direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        bulletTimer -= Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float rotation = rotationSpeed * Mathf.Rad2Deg * horizontal * Time.deltaTime;
        transform.Rotate(0,0,-rotation, Space.Self);

        
        angle = Mathf.Deg2Rad * transform.eulerAngles.z;
        direction = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));  

        if(Input.GetKeyDown(KeyCode.Space) && bulletTimer <= 0) {
            Instantiate(bullet, new Vector3(transform.position.x+direction.x, transform.position.y+direction.y,0), transform.rotation);
            bulletTimer = ShootInterval;
        }


    }

    void FixedUpdate() {
        if(isDead) return;
        if(vertical > 0) {
             if(rigidbody2d.velocity.magnitude <= 10.0f) {
                 if(vertical > 0) Propulsion.Play();
                float forceX = scale * speed * vertical * direction.x * Time.fixedDeltaTime;
                float forceY = scale * speed * vertical * direction.y * Time.fixedDeltaTime;
                // Debug.Log(forceX + ", " + forceY + ", " + angle);

                Vector2 force = new Vector2(forceX, forceY);
                rigidbody2d.AddForce(force);
            }
            
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Asteroids")) {
            isDead = true;
            GameController.isRunning = false;
            animator.Play("ShipDeath");   
        }
    }

    public static void ResetPlayer(GameObject player) {
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(0,0,0);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        PlayerController.isDead = false;
    }
}
