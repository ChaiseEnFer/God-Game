using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private spawnBuilding spawnBuildingScript;

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

    //variables publiques
    public GameObject SelectedCharacter;
    public int EntitiesNumber = 0;
    public int FoodQuantity;
    public bool IsDayRunning = false;
    public int ActualHappiness;
    public int WoodQuantity;
    public int StoneQuantity;

    //variables privées
    private int _maxHappiness;

    //Methodes a appeler a chaque fin de jour

    /// <summary>
    /// Cette méthode vérifie pour chaque people fatigué, s'il y a un lit de disponible et lui permet de se reposer
    /// </summary>
    public void CheckForHouses()
    {
        List<GameObject> _availableHouses = spawnBuildingScript.houseList;
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
                        people.GetComponent<Population>().HasAHouse = true;
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
    public void CheckIfEnoughFood()
    {
        if (FoodQuantity > AllPeople.Count)
        {
            FoodQuantity -= AllPeople.Count;
        }
        else
        {
            for (int i = 0; i < FoodQuantity; i++)
            {
                int randomIndex = Random.Range(0, AllPeople.Count);

                GameObject peopleToDestroy = AllPeople[randomIndex];
                AllPeople.Remove(peopleToDestroy);
                Destroy(peopleToDestroy);
                FoodQuantity = 0;
            }
        }
    }

    /// <summary>
    /// Ajoute le bonheur selon le nombre de batiments qui en produisent
    /// </summary>
    public void AddHappiness() // A rédiger une fois qu'on aura merge le taf de victor
    {
        foreach (GameObject build in spawnBuildingScript.buildList)
        {
            if (build.tag == "library")
            {
                Debug.Log(build.GetComponent<libraryBuildInfo>().happynessGiven.ToString());
            }
            if (build.tag == "museum")
            {
                Debug.Log(build.GetComponent<museumBuildInfo>().happynessGiven.ToString());
            }
        }
    }

    //Conditions de fin de partie

    /// <summary>
    /// Verifie la condition de décimation de la population
    /// </summary>
    public void CheckForDecimatedPopulation()
    {
        if (AllPeople.Count <= 0)
        {
            //FIN DE PARTIE - DEFAITE
        }
    }

    /// <summary>
    /// Verifie la condition de victoire : bonheur au max
    /// </summary>
    public void CheckForHappiness()
    {
        if (ActualHappiness >= _maxHappiness)
        {
            //FIN DE PARTIE - VICTOIRE
        }
    }
}
