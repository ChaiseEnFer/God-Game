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
    private GameObject _buildParent = null;

    private GameObject _preview = null;
    private GameObject _buildSelected = null;
    private Camera _cam = null;
    private bool _buildMode = false;
    private float _timerPreview = 0.0f;

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

            BuildPreview();
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
    }
    public void ExitBuildMode()
    {
        _buildMode = false;
    }


    private void BuildPreview()
    {
        if (_timerPreview > 0.01f)
        {
            Destroy(_preview);
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag != "build")
                {
                    _preview = Instantiate(_buildSelected, hit.point, Quaternion.identity);
                }
            }
            _timerPreview = 0.0f;
        }
        else
        {
            _timerPreview += Time.deltaTime;
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
                GameObject newBuild = Instantiate(_buildSelected, hit.point, Quaternion.identity);
                newBuild.transform.parent = _buildParent.transform;
            }
        }
    }
}
