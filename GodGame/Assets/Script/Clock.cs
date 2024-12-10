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

    public int ActualDay = 1;

    private bool _isActivated = false;

    private void Start()
    {
        ActualDay = 1;
    }

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
            if (GameManager.Instance.IsDayRunning)
                AddRessources();
            ActualMinute = 00.00f;

            if (ActualHour == _dayStartHour - 1 || ActualHour == _dayFinishHour - 1)
            {
                _isActivated = false;
            }

            if (ActualHour == _dayFinishHour && _isActivated == false)
            {
                GameManager.Instance.IsDayRunning = false;
                _isActivated = true;

                UpdateBuildings();

                GameManager.Instance.MakeThemExhausted();
                GameManager.Instance.CheckForHouses();
                GameManager.Instance.CheckIfEnoughFood();
                GameManager.Instance.AddHappiness();
                GameManager.Instance.CheckForHappiness();

                foreach (GameObject people in GameManager.Instance.AllPeople)
                {
                    people.GetComponent<PeopleProperties>().Age++;
                    people.GetComponent<PeopleProperties>().CheckForAge();
                }
            }

            if (ActualHour == _dayStartHour && _isActivated == false)
            {
                GameManager.Instance.IsDayRunning = true;
                foreach (GameObject people in GameManager.Instance.AllPeople)
                {
                    people.GetComponent<Population>().HasAHouse = false;
                    people.GetComponent<Population>().CanMove = true;
                    people.GetComponent<Population>().IsDestinationSet = false;
                }
                _isActivated = true;
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
    }

    private void AddRessources()
    {
        foreach (GameObject worker in GameManager.Instance.FoodHarvesters)
        {
            if (!worker.GetComponent<PeopleProperties>().IsTired)
                GameManager.Instance.FoodQuantity++;
        }
        foreach (GameObject worker in GameManager.Instance.Timbers)
        {
            if (!worker.GetComponent<PeopleProperties>().IsTired)
                GameManager.Instance.WoodQuantity++;
        }
        foreach (GameObject worker in GameManager.Instance.Miners)
        {
            if (!worker.GetComponent<PeopleProperties>().IsTired)
                GameManager.Instance.StoneQuantity++;
        }
    }

    private void UpdateBuildings()
    {
        GameObject[] prebuild;
        if (GameObject.FindGameObjectsWithTag("UnderConstruction") != null)
        {
            prebuild = GameObject.FindGameObjectsWithTag("UnderConstruction");

            foreach (GameObject build in prebuild)
            {
                build.GetComponent<underConstructionBuildManager>().BuildUntilNight();
            }
        }
    }
}
