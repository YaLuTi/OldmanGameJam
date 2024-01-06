using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class cameraVibration : MonoBehaviour
{
    static float VibrationFreq = 0;
    CinemachineVirtualCamera vcam;
    static float t = 1;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp( VibrationFreq, 0, t);
        t += 2 * Time.deltaTime;
    }

    public static void ShakeCamera()
    {
        t = 0;
        VibrationFreq = 0.4f;
    }
}
