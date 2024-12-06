using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float Minute = 00.00f;
    public int Hour = 0;

    // Update is called once per frame
    void Update()
    {
        UpdateDayTime();
    }

    private void UpdateDayTime()
    {
        if(Minute >= 60)
        {
            Hour += 1;
            Minute = 00.00f;
        }
        else
        {
            Minute += Time.deltaTime;
        }
    }
}
