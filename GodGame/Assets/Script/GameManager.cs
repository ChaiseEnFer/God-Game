using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Singleton
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

    //Lists than contains people according to their jobs
    public List<GameObject> AllPeople = new();

    public List<GameObject> Wanderers = new();
    public List<GameObject> FoodHarvesters = new();
    public List<GameObject> Timbers = new();
    public List<GameObject> Miners = new();
    public List<GameObject> Masons = new();

    //publics variables
    public GameObject SelectedCharacter;
    public int EntitiesNumber = 0;
    public int FoodQuantity;
    public bool IsDayRunning = false;
    public int ActualHappiness;
    public int WoodQuantity;
    public int StoneQuantity;

    //privates variables
    [SerializeField]
    private spawnBuilding spawnBuildingScript;
    private readonly int _maxHappiness = 100;

    /// <summary>
    /// Method that attribute or reattribute a job to people
    /// </summary>
    /// <param name="job">The new job</param>
    public void SendToSchool(int job)
    {
        List<GameObject> _allSchools = new();
        foreach (GameObject build in spawnBuildingScript.buildList)
        {
            if (build.CompareTag("school"))
            {
                if (build.GetComponent<schoolBuildInfo>().IsFree == true)
                {
                    build.GetComponent<schoolBuildInfo>().CurrentOccupants++;

                    int _lastJob = SelectedCharacter.GetComponent<PeopleProperties>().Job;
                    RemoveAffiliation(_lastJob, SelectedCharacter);
                    SelectedCharacter.GetComponent<PeopleProperties>().Job = job;
                    AddAffiliation(job);
                    SelectedCharacter.GetComponent<PeopleProperties>().ChangeSkin();

                    SelectedCharacter.GetComponent<Population>().TargetPs = build.transform.position;


                    if (build.GetComponent<schoolBuildInfo>().CurrentOccupants == build.GetComponent<schoolBuildInfo>().MaxOccupants)
                        build.GetComponent<schoolBuildInfo>().IsFree = false;

                    SelectedCharacter.GetComponent<Population>().CanMove = false;
                    return;
                }
            }
        }
    }

    //Those methods will update the list the people is in

    private void RemoveAffiliation(int job, GameObject people)
    {
        switch (job)
        {
            case 0:
                Wanderers.Remove(people);
                break;
            case 1:
                FoodHarvesters.Remove(people);
                break;
            case 2:
                Timbers.Remove(people);
                break;
            case 3:
                Miners.Remove(people);
                break;
            case 4:
                Masons.Remove(people);
                break;
        }
    }

    private void AddAffiliation(int _newJob)
    {
        switch (_newJob)
        {
            default:
                Wanderers.Add(SelectedCharacter);
                break;
            case 1:
                FoodHarvesters.Add(SelectedCharacter);
                break;
            case 2:
                Timbers.Add(SelectedCharacter);
                break;
            case 3:
                Miners.Add(SelectedCharacter);
                break;
            case 4:
                Masons.Add(SelectedCharacter);
                break;
        }
    }

    //Call every end of day

    /// <summary>
    /// Make workers tired
    /// </summary>
    public void MakeThemExhausted()
    {
        foreach (GameObject worker in GameManager.Instance.FoodHarvesters)
        {
            worker.GetComponent<PeopleProperties>().IsTired = true;
        }
        foreach (GameObject worker in GameManager.Instance.Timbers)
        {
            worker.GetComponent<PeopleProperties>().IsTired = true;
        }
        foreach (GameObject worker in GameManager.Instance.Miners)
        {
            worker.GetComponent<PeopleProperties>().IsTired = true;
        }
        foreach (GameObject worker in GameManager.Instance.Masons)
        {
            worker.GetComponent<PeopleProperties>().IsTired = true;
        }
    }

    /// <summary>
    /// This method checks if there is a bed available for each tired person and allows them to rest.
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
                    foreach(GameObject worker in AllPeople) //Lower the happiness for each tired worker without bed
                    {
                        if (worker.GetComponent<PeopleProperties>().IsTired)
                        {
                            if (ActualHappiness > 0)
                            {
                                ActualHappiness--;
                                UIManager.Instance.UpdateSlider();
                            }
                        }
                    }
                    return;
                }


                else
                {

                    GameObject house = _availableHouses[0];
                    people.GetComponent<PeopleProperties>().IsTired = false;
                    people.GetComponent<Population>().HasAHouse = true;
                    people.GetComponent<PeopleProperties>().Tireness = 100;
                    people.GetComponent<Population>().TargetPs = house.transform.position;
                    if (house.GetComponent<houseBuildInfo>().currentPeopleIn == house.GetComponent<houseBuildInfo>().maxPeopleIn-1)
                    {
                        _availableHouses.Remove(house);
                    }
                    else
                    {
                        house.GetComponent<houseBuildInfo>().currentPeopleIn++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// This method checks that no one is lacking food and if not, they die
    /// </summary>
    public void CheckIfEnoughFood()
    {
        if (FoodQuantity > AllPeople.Count)
        {
            FoodQuantity -= AllPeople.Count;
        }
        else
        {
            int rounds = AllPeople.Count - FoodQuantity;
            
            for (int i = 0; i < rounds; i++)
            {
                Debug.Log(AllPeople.Count);
                int randomIndex = Random.Range(0, AllPeople.Count);

                GameObject peopleToDestroy = AllPeople[randomIndex];
                AllPeople.Remove(peopleToDestroy);
                RemoveAffiliation(peopleToDestroy.GetComponent<PeopleProperties>().Job, peopleToDestroy);
                peopleToDestroy.GetComponent<PeopleProperties>().Death();
                

                if (ActualHappiness > 0)
                {
                    ActualHappiness--;
                    UIManager.Instance.UpdateSlider();
                }
                EntitiesNumber--;
            }
            if (AllPeople.Count <= 0)
            {
                Instance.GetComponent<MainMenuHandler>().LoadScene("LoseScene");
            }
            FoodQuantity = 0;
        }
    }

    /// <summary>
    /// Add happiness based on the number of buildings that produce it.
    /// </summary>
    public void AddHappiness()
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
            UIManager.Instance.UpdateSlider();
        }

    }

    //End game conditions

    /// <summary>
    /// Check wincon : ma happiness
    /// </summary>
    public void CheckForHappiness()
    {
        UIManager.Instance.UpdateSlider();

        if (ActualHappiness >= _maxHappiness)
        {
            Instance.GetComponent<MainMenuHandler>().LoadScene("WinScene");
        }
    }

    public void EntrerFpsView()
    {
        SelectedCharacter.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    public void ExitFpsView()
    {
        SelectedCharacter.GetComponent<CinemachineVirtualCamera>().enabled = false;
    }
}
