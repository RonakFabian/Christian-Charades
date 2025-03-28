using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    bool isenabled = false;
    void Start()
    {
        // Screen.orientation = ScreenOrientation.LandscapeRight;
        isenabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!isenabled)
        {
            enabled = true;

            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                print("L");
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
        }

        if (!isenabled)
        {
            enabled = true;
            if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {

                print("R");
                Screen.orientation = ScreenOrientation.LandscapeRight;

            }
        }




    }


}

