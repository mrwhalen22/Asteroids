using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject Player;
    public static float asteroidTimer;
    public float spawnTime;

    public float width = 16f;
    public float height = 8f;

    public static bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        asteroidTimer = 0;
        spawnTime = 7.5f;
        SpawnAsteroid();
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            ResetGame();
            PlayerController.ResetPlayer(Player);
            SpawnAsteroid();
        }


        if(isRunning) {
            asteroidTimer+=Time.deltaTime;
            if(asteroidTimer >= spawnTime) {
                SpawnAsteroid();
            }
        }
    }

    void SpawnAsteroid() {
        asteroidTimer = 0;
        Vector3 newPos =  new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
        while(Vector3.Distance(newPos, Player.transform.position) < 2f) {
            newPos =  new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
        }
        GameObject newAsteroid = GameObject.Instantiate(asteroid, newPos, Quaternion.identity);
        if(newAsteroid != null) newAsteroid.GetComponent<AsteroidController>().generation = 0;
    }

    public static void ResetGame() {
        isRunning = true;
        GameTimer.GameTime = 0;
        foreach(GameObject asteroid in FindObjectsOfType<GameObject>()) {
            if(asteroid.GetComponent<AsteroidController>() != null) Destroy(asteroid);
        }
    }
}
