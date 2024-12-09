using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class spawnBuilding : MonoBehaviour
{
    [SerializeField] 
    private GameObject _house = null;
    [SerializeField]
    private GameObject _school = null;
    [SerializeField]
    private GameObject _farm = null;
    [SerializeField]
    private GameObject _library = null;
    [SerializeField]
    private GameObject _museum = null;
    [SerializeField]
    private GameObject _preview = null;
    [SerializeField]
    private GameObject _underConstruction = null;

    [SerializeField]
    private LayerMask _LayerM;

    [SerializeField]
    private GameObject _buildParent = null;

    [SerializeField]
    private Camera _cam = null;

    private GameObject _previewSave = null;
    private GameObject _buildSelected;
    public bool IsInBuildMode = false;

    public List<GameObject> buildList = new List<GameObject>();
    public List<GameObject> houseList = new List<GameObject>();

    private int _woodNeeded;
    private int _stoneNeeded;

    private void Start()
    {
        _buildSelected = _house;
    }

    private void Update()
    {
        if (IsInBuildMode)
        {
            SpawnAtMousePos();
            BuildPreviewUpdate();
        }

        else
        {
            SelectPeople();
        }
    }

    /// <summary>
    /// Allow selection of population by clicking
    /// </summary>
    public void SelectPeople()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "People")
                {
                    GameManager.Instance.SelectedCharacter = hit.collider.gameObject;

                    CheckForDisplay();
                }
            }
        }
    }

    public void CheckForDisplay()
    {
        foreach (GameObject build in buildList)
        {
            if (build.tag == "school")
            {
                UIManager.Instance.DisplaySchoolPanel();
            }
            else
            {
                UIManager.Instance.HideSchoolPanel();
            }
        }
        UIManager.Instance.DisplayPeopleInfoPanel();
    }

    //Choose building type

    public void ChangeBuildToHouse()
    {
        _buildSelected = _house;
    }
    public void ChangeBuildToSchool()
    {
        _buildSelected = _school;
    }
    public void ChangeBuildToFarm()
    {
        _buildSelected = _farm;
    }
    public void ChangeBuildToLibrary()
    {
        _buildSelected = _library;
    }
    public void ChangeBuildToMuseum()
    {
        _buildSelected = _museum;
    }

    //Build Mode (enter / exit)

    public void EnterBuildMode()
    {
        IsInBuildMode = true;
        if (_previewSave == null)
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _previewSave = Instantiate(_preview, hit.point, Quaternion.identity);
                _previewSave.transform.parent = transform;
            }
        }
    }

    public void ExitBuildMode()
    {
        IsInBuildMode = false;
        if (_previewSave != null)
        {
            Destroy(_previewSave);
        }
    }

    /// <summary>
    /// Make a preview of the position for the new build
    /// </summary>
    private void BuildPreviewUpdate()
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "buildingZone")
            {
                if (_previewSave != null)
                {
                    _previewSave.transform.position = hit.point;
                }
                else
                {
                    _previewSave = Instantiate(_preview, hit.point, Quaternion.identity);
                    _previewSave.transform.parent = transform;
                }
            }
            else 
            {
                if (_previewSave != null)
                {
                    Destroy(_previewSave);
                }
            }
        }
    }

    /// <summary>
    /// Spawns new builds
    /// </summary>
    private void SpawnAtMousePos()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "buildingZone" && !EventSystem.current.IsPointerOverGameObject())
                {
                    CheckForRessources();
                    if (_woodNeeded < GameManager.Instance.WoodQuantity && _stoneNeeded < GameManager.Instance.StoneQuantity)
                    {
                        List<GameObject> _availableMasons = new();

                        foreach (GameObject mason in GameManager.Instance.Masons)
                        {
                            if (mason.GetComponent<Population>().CanMove == true && !mason.GetComponent<PeopleProperties>().IsTired)
                            {
                                _availableMasons.Add(mason);
                            }
                        }

                        if (_availableMasons.Count > 0) //Check if a mason is available
                        {
                            //Build the monument

                            GameObject newBuild = Instantiate(_underConstruction, hit.point, Quaternion.identity);
                            newBuild.transform.parent = _buildParent.transform;
                            newBuild.GetComponent<underConstructionBuildManager>().buildObjective = _buildSelected;
                            newBuild.GetComponent<underConstructionBuildManager>().buildParent = _buildParent;
                            newBuild.GetComponent<underConstructionBuildManager>().parent = gameObject;
                            buildList.Add(newBuild);

                            //assign a mason to it

                            GameObject _randomMason = _availableMasons[Random.Range(0, GameManager.Instance.Masons.Count - 1)];
                            _randomMason.GetComponent<Population>().CanMove = false;
                            _randomMason.GetComponent<Population>().TargetPs = newBuild.transform.position;

                            //collect the price

                            GameManager.Instance.WoodQuantity -= _woodNeeded;
                            GameManager.Instance.StoneQuantity -= _stoneNeeded;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Check if the needed ressources are available to build
    /// </summary>
    private void CheckForRessources()
    {
        switch (_buildSelected.name)
        {
            case "House":
                _woodNeeded = _buildSelected.GetComponent<houseBuildInfo>().woodPrice;
                _stoneNeeded = _buildSelected.GetComponent<houseBuildInfo>().stonePrice;
                break;
            case "Farm":
                _woodNeeded = _buildSelected.GetComponent<farmBuildInfo>().woodPrice;
                _stoneNeeded = _buildSelected.GetComponent<farmBuildInfo>().stonePrice;
                break;
            case "school":
                _woodNeeded = _buildSelected.GetComponent<schoolBuildInfo>().woodPrice;
                _stoneNeeded = _buildSelected.GetComponent<schoolBuildInfo>().stonePrice;
                break;
            case "Museum":
                _woodNeeded = _buildSelected.GetComponent<museumBuildInfo>().woodPrice;
                _stoneNeeded = _buildSelected.GetComponent<museumBuildInfo>().stonePrice;
                break;
            case "Library":
                _woodNeeded = _buildSelected.GetComponent<libraryBuildInfo>().woodPrice;
                _stoneNeeded = _buildSelected.GetComponent<libraryBuildInfo>().stonePrice;
                break;
            default:
                Debug.Log("CA MARCHE PAS PUTAIN");
                _woodNeeded = 1000000;
                _stoneNeeded = 1000000;
                break;
        }
    }
}
