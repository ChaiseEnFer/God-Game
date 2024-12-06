using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private int _dayStartHour = 8;

    [SerializeField]
    private int _dayFinishHour = 22;

    public float ActualMinute = 00.00f;

    public int ActualHour = 0;

    public int ActualDay = 0;

    private bool _isDayFinished = false;

    private bool _isActivated = false;
    // Update is called once per frame
    void Update()
    {
        UpdateDayTime();
    }

    private void UpdateDayTime()
    {
        if(ActualMinute >= 60)
        {
            ActualHour += 1;
            ActualMinute = 00.00f;

            if(ActualHour == _dayFinishHour)
            {
                _isDayFinished=true;

            }

            if (ActualHour == _dayStartHour)
            {
                _isDayFinished = false;
                _isActivated=true;
            }

            if (ActualHour >= 24)
            {
                ActualHour = 0;
                ActualDay += 1;
            }
        }
        else
        {
            ActualMinute += Time.deltaTime*10;
        }

        if(_isDayFinished && !_isActivated)
        {
            GameManager.Instance.CheckIfEnoughFood();
            GameManager.Instance.CheckForHouses();
            _isActivated = true;
        }
    }
}
