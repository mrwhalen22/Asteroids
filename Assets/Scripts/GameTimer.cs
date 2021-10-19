using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameTimer : MonoBehaviour
{
    [SerializeField] public static float GameTime;
    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        GameTime = 0; 
        text = GetComponent<Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.isDead || !GameController.isRunning) {return;}
        GameTime += Time.deltaTime;
        text.text = string.Format("{0,7:F2}s", GameTime);
    }
}
