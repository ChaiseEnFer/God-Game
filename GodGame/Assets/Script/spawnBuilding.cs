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
    private GameObject _buildParent = null;

    [SerializeField]
    private Camera _cam = null;

    private GameObject _previewSave = null;
    private GameObject _buildSelected = null;
    private bool _buildMode = false;

    public List<GameObject> buildList = new List<GameObject>();
    public List<GameObject> houseList = new List<GameObject>();

    private void Start()
    {
        _buildSelected = _house;
    }

    private void Update()
    {

        if (_buildMode)
        {
            SpawnAtMousePos();
            BuildPreviewUpdate();
        }

        else
        {
            SelectPeople();
        }
    }

    public void SelectPeople()
    {
        Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            _previewSave = Instantiate(_preview, hit.point, Quaternion.identity);
            _previewSave.transform.parent = transform;
        }
    }

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
    public void EnterBuildMode()
    {
        _buildMode = true;
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
        _buildMode = false;
        if (_previewSave != null)
        {
            Destroy(_previewSave);
        }
    }

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
                    GameObject newBuild = Instantiate(_underConstruction, hit.point, Quaternion.identity);
                    newBuild.transform.parent = _buildParent.transform;
                    newBuild.GetComponent<underConstructionBuildManager>().buildObjective = _buildSelected;
                    newBuild.GetComponent<underConstructionBuildManager>().buildParent = _buildParent;
                    newBuild.GetComponent<underConstructionBuildManager>().parent = gameObject;
                    buildList.Add(newBuild);
                }
            }
        }
    }
}
