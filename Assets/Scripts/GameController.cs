using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject Player;
    public float asteroidTimer;
    public float spawnTime;

    public float width = 16f;
    public float height = 8f;

    // Start is called before the first frame update
    void Start()
    {
        asteroidTimer = 0;
        spawnTime = 7.5f;
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        asteroidTimer+=Time.deltaTime;
        if(asteroidTimer >= spawnTime) {
            SpawnAsteroid();
            asteroidTimer = 0;
        }
    }

    void SpawnAsteroid() {
        Vector3 newPos =  new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
        while(Vector3.Distance(newPos, Player.transform.position) < 2f) {
            newPos =  new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
        }
        GameObject new1 = GameObject.Instantiate(asteroid, newPos, Quaternion.identity);
        AsteroidController new1Controller = new1.GetComponent<AsteroidController>();
        new1Controller.generation = 0;
    }
}
