using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class TopDownCameraManager : MonoBehaviour
{
    [SerializeField]
    private float _cameraSpeed = 0.0f;

    public InputAction cameraControls;
    public InputAction cameraZoom;
    private Vector2 cameraMove = Vector2.zero;
    private Vector2 cameraMoveWithBorder = Vector2.zero;

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
            cameraMoveWithBorder.x = -1;
        }
        if (Input.mousePosition.x > Screen.width * 0.9)
        { 
            cameraMoveWithBorder.x = 1;
        }
        if (Input.mousePosition.y < Screen.height / 10)
        {
            cameraMoveWithBorder.y = -1;
        }
        if (Input.mousePosition.y > Screen.height * 0.9)
        {
            cameraMoveWithBorder.y = 1;
        }
        cameraMoveWithBorder *= Time.deltaTime * _cameraSpeed;
        cameraMove = cameraControls.ReadValue<Vector2>() * Time.deltaTime * _cameraSpeed;
        if (cameraMove != Vector2.zero)
        {
            transform.position = new Vector3(transform.position.x + cameraMove.x, transform.position.y + Mouse.current.scroll.ReadValue().normalized.y, transform.position.z + cameraMove.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + cameraMoveWithBorder.x, transform.position.y + Mouse.current.scroll.ReadValue().normalized.y, transform.position.z + cameraMoveWithBorder.y);
            cameraMoveWithBorder = Vector2.zero;
        }



    }

}
