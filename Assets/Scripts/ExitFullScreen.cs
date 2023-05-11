using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFullScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true && Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
            Debug.Log("Exit full screen");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) == true && Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
            Debug.Log("Enter full screen");
        }    
    }
}
