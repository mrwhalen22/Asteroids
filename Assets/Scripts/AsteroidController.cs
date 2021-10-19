using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public float speed, rSpeed;
    public int generation;
    public Vector2 direction;
    public Vector3 offset;
    public bool destroyed;

    public GameObject prefab;
    [SerializeField] private ParticleSystem Explosion;
  

    // Start is called before the first frame update
    void Start()
    {
        Explosion = GetComponentInChildren<ParticleSystem>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        speed = Random.Range(010,300) / 100.0f;
        rSpeed = Random.Range(2,10);
        direction = new Vector2(Random.Range(0,10), Random.Range(0,10));
        direction.Normalize();
        rigidbody2d.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Explosion.isPlaying && destroyed) Destroy(gameObject);
        
        transform.Rotate(0,0,rSpeed * Time.deltaTime);
        if(generation == 3) {
            destroyed = true;
        }

       
    }

    void Split() {
        Vector2 perp = Vector2.Perpendicular(direction);
        offset = new Vector3(perp.x,perp.y,0);
        GameObject new1 = GameObject.Instantiate(prefab, transform.position + offset, Quaternion.identity);
        GameObject new2 = GameObject.Instantiate(prefab, transform.position - offset, Quaternion.identity);
        new1.transform.localScale = new Vector3(transform.localScale.x * 0.75f, transform.localScale.y * 0.75f, 1);
        new2.transform.localScale = new Vector3(transform.localScale.x * 0.75f, transform.localScale.y * 0.75f, 1);
        AsteroidController new1Controller = new1.GetComponent<AsteroidController>();
        AsteroidController new2Controller = new2.GetComponent<AsteroidController>();
        new1Controller.generation = generation + 1;
        new2Controller.generation = generation + 1;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullets")) {
            Explosion.Play();
            Split();
            destroyed = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
