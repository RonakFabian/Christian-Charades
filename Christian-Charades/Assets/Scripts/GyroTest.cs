using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using TMPro;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    Vector3 rot;
    public TextMeshPro txt;
    Gyroscope m_Gyro;


    void Start()
    {
        rot = Vector3.zero;
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        Input.gyro.enabled = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    // Update is called once per frame
    void Update()
    {
        rot.x = Input.gyro.rotationRateUnbiased.x;
        transform.Rotate(rot);
        print(rot);
    }

    void OnGUI()
    {
        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + m_Gyro);
        GUI.Label(new Rect(500, 350, 200, 40), "Gyro attitude" + m_Gyro.attitude);
        GUI.Label(new Rect(500, 400, 200, 40), "Gyro enabled : " + m_Gyro.enabled);
    }

}
