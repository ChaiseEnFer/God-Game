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
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void CheckForHouses()
    {
        List<GameObject> _availableHouses = new();
        foreach (GameObject people in AllPeople)
        {
            if (people.GetComponent<PeopleProperties>().IsTired )
            {
                foreach (GameObject house in _availableHouses)
                {
                    people.GetComponent<PeopleProperties>().IsTired = false;
                    people.GetComponent<PeopleProperties>().Tireness = 100;
                    _availableHouses.Remove(house);
                }
            }
        }
    }
}
