using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //Listes contenant les peoples selon leur métier
    public List<GameObject> AllPeople = new();

    public List<GameObject> Wanderers = new();
    public List<GameObject> FoodHarvesters = new();
    public List<GameObject> Timbers = new();
    public List<GameObject> Miners = new();
    public List<GameObject> Masons = new();

    //variables
    public GameObject SelectedCharacter;
    public int EntitiesNumber = 0;
    public int FoodQuantity;
    public bool IsDayRunning = false;

    /// <summary>
    /// Cette méthode vérifie pour chaque people fatigué, s'il y a un lit de disponible et lui permet de se reposer
    /// </summary>
    private void CheckForHouses()
    {
        List<GameObject> _availableHouses = new();
        foreach (GameObject people in AllPeople)
        {
            if (people.GetComponent<PeopleProperties>().IsTired )
            {
                if (_availableHouses.Count == 0)
                {
                    return;
                }
                else
                {
                    foreach (GameObject house in _availableHouses)
                    {
                        people.GetComponent<PeopleProperties>().IsTired = false;
                        people.GetComponent<PeopleProperties>().Tireness = 100;
                        people.GetComponent<Population>().TargetPs = house.transform.position;
                        _availableHouses.Remove(house);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Cette méthode vérifie que personne ne manque de nourriture et sinon les tuent
    /// </summary>
    private void CheckIfEnoughFood()
    {

    }
}
