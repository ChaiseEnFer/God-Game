using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class TopDownCameraManager : MonoBehaviour
{
    [SerializeField]
    private float _cameraSpeed = 0.0f;
    [SerializeField]
    private float _cameraZoomSpeed = 0.0f;

    public InputAction cameraControls;
    public InputAction cameraZoom;
    private Vector2 _cameraMove = Vector2.zero;
    private Vector2 _cameraMoveWithBorder = Vector2.zero;
    private float _cameraZoom = 0;

    private void OnEnable()
    {
        cameraControls.Enable();
    }
    private void OnDisable()
    {
        cameraControls.Disable();
    }



    void Update()
    {
        if (Input.mousePosition.x < Screen.width / 10)
        {
            _cameraMoveWithBorder.x = -1;
        }
        if (Input.mousePosition.x > Screen.width * 0.9)
        { 
            _cameraMoveWithBorder.x = 1;
        }
        if (Input.mousePosition.y < Screen.height / 10)
        {
            _cameraMoveWithBorder.y = -1;
        }
        if (Input.mousePosition.y > Screen.height * 0.9)
        {
            _cameraMoveWithBorder.y = 1;
        }
        _cameraMoveWithBorder *= Time.deltaTime * _cameraSpeed;
        _cameraMove = cameraControls.ReadValue<Vector2>() * Time.deltaTime * _cameraSpeed;
        _cameraZoom = Mouse.current.scroll.ReadValue().normalized.y * Time.deltaTime * _cameraZoomSpeed;
        if (_cameraMove != Vector2.zero)
        {
            transform.position = new Vector3(transform.position.x + _cameraMove.x, transform.position.y + _cameraZoom, transform.position.z + _cameraMove.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + _cameraMoveWithBorder.x, transform.position.y + _cameraZoom, transform.position.z + _cameraMoveWithBorder.y);
            _cameraMoveWithBorder = Vector2.zero;
        }



    }

}
