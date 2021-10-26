using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool stop;
    public float stopTime;

    public Transform shackCam;
    public Vector3 shake;

    public void StopTime()
    {
        if (!stop)
        {
            stop = true;
            shackCam.localPosition = shake;
            Time.timeScale = 0;

            StartCoroutine("ReturnTimeScale");
        }
    }
    IEnumerator ReturnTimeScale()
    {
        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = 1;
        shackCam.localPosition = Vector3.zero;
        stop = false;
    }
}
