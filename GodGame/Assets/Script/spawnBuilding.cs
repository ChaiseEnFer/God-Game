using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class spawnBuilding : MonoBehaviour
{
    [SerializeField] 
    private GameObject _house = null;
    [SerializeField]
    private GameObject _school = null;
    [SerializeField]
    private GameObject _preview = null;

    [SerializeField]
    private GameObject _buildParent = null;

    private GameObject _previewSave = null;
    private GameObject _buildSelected = null;
    private Camera _cam = null;
    private bool _buildMode = false;

    private void Start()
    {
        _cam = Camera.main;
        _buildSelected = _house;
    }

    private void Update()
    {
        if (_buildMode)
        {
            SpawnAtMousePos();

            BuildPreviewUpdate();
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
            if (hit.collider.tag != "build")
            {
                _previewSave.transform.position = hit.point;
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
                if (hit.collider.tag != "build")
                {
                    GameObject newBuild = Instantiate(_buildSelected, hit.point, Quaternion.identity);
                    newBuild.transform.parent = _buildParent.transform;
                }
            }
        }
    }
}
