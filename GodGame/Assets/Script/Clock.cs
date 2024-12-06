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

    /// <summary>
    /// Permet de compter les heures puis dincrémenter les jours, de définir les horaires de travail et de dire quand il est lheure de se reposer
    /// </summary>
    private void UpdateDayTime()
    {
        if(ActualMinute >= 60)
        {
            ActualHour += 1;
            ActualMinute = 00.00f;

            if(ActualHour == _dayFinishHour)
            {
                GameManager.Instance.IsDayRunning = false;

            }

            if (ActualHour == _dayStartHour)
            {
                GameManager.Instance.IsDayRunning = true;
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

        if(!GameManager.Instance.IsDayRunning && !_isActivated)
        {
            GameManager.Instance.CheckIfEnoughFood();
            GameManager.Instance.CheckForHouses();

            foreach (GameObject people in GameManager.Instance.AllPeople)
            {
                people.GetComponent<PeopleProperties>().Age++;
                people.GetComponent<PeopleProperties>().CheckForAge();
            }

            _isActivated = true;
        }
    }


}
