using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapScreenEntity : MonoBehaviour
{
    private Camera mainCam;
    private Rect cameraBounds;
    private Renderer[] renderers;
    private bool wrappingX = false, wrappingY = false, isVisible;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        cameraBounds = mainCam.rect;
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenWrap();
    }

    void ScreenWrap() {
        
        Vector3 playerVpos = mainCam.WorldToViewportPoint(transform.position);
        if(playerVpos.x <= 1 && playerVpos.x >= 0 && playerVpos.y <= 1 && playerVpos.y >= 0) { 
            wrappingX = false;
            wrappingY = false;
            return;
        }
        if(wrappingX || wrappingY) return;

        Vector3 newPosition = transform.position;
        if(playerVpos.x > 1 || playerVpos.x < 0) {
            newPosition.x = -newPosition.x;
            wrappingX = true;
        }
        if(playerVpos.y > 1 || playerVpos.y < 0) {
            newPosition.y = -newPosition.y;
            wrappingY = true;
        }

        transform.position = newPosition;
    }

    bool CheckVisible() {
        foreach(Renderer r in renderers) {
            if(r.isVisible) return true;
        }
        return false;
    }
}
