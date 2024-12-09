using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class TopDownCameraManager : MonoBehaviour
{

    public float CameraSpeed = 0.0f;
    [SerializeField]
    private float _cameraZoomSpeed = 0.0f;
    [SerializeField]
    private float limitCamUp = 0.0f;
    [SerializeField]
    private float limitCamRight = 0.0f;
    [SerializeField]
    private float limitCamDown = 0.0f;
    [SerializeField]
    private float limitCamLeft = 0.0f;
    [SerializeField]
    private float limitCamZoomUp = 0.0f;
    [SerializeField]
    private float limitCamZoomDown = 0.0f;

    public InputAction cameraControls;
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

    private void Awake()
    {
        
    }

    void Update()
    {
        if (Input.mousePosition.x < Screen.width / 100)
        {
            _cameraMoveWithBorder.x = -1;
        }
        if (Input.mousePosition.x > Screen.width * 0.99)
        { 
            _cameraMoveWithBorder.x = 1;
        }
        if (Input.mousePosition.y < Screen.height / 100)
        {
            _cameraMoveWithBorder.y = -1;
        }
        if (Input.mousePosition.y > Screen.height * 0.99)
        {
            _cameraMoveWithBorder.y = 1;
        }
        _cameraMoveWithBorder *= Time.deltaTime * CameraSpeed;
        _cameraMove = cameraControls.ReadValue<Vector2>() * Time.deltaTime * CameraSpeed;
        _cameraZoom = -Mouse.current.scroll.ReadValue().normalized.y * Time.deltaTime * _cameraZoomSpeed;
        if (_cameraMove != Vector2.zero)
        {
            float x = Mathf.Clamp(transform.position.x + _cameraMove.x,limitCamLeft,limitCamRight);
            float y = Mathf.Clamp(transform.position.y + _cameraZoom, limitCamZoomDown, limitCamZoomUp);
            float z = Mathf.Clamp(transform.position.z + _cameraMove.y, limitCamUp, limitCamDown);
            transform.position = new Vector3(x,y,z);
        }
        else
        {
            float x = Mathf.Clamp(transform.position.x + _cameraMoveWithBorder.x, limitCamLeft, limitCamRight);
            float y = Mathf.Clamp(transform.position.y + _cameraZoom, limitCamZoomDown, limitCamZoomUp);
            float z = Mathf.Clamp(transform.position.z + _cameraMoveWithBorder.y, limitCamUp, limitCamDown);
            transform.position = new Vector3(x, y, z);
            _cameraMoveWithBorder = Vector2.zero;
        }



    }

}
