using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private enum Mode
    {
        Horizontal,
        Free
    }

    [SerializeField]
    private Mode _mode = Mode.Horizontal;

    [SerializeField]
    private float _movementSpeed = 10f;

    [SerializeField]
    private float _rotationSpeed = 5f;

    private float _xRotation;
    private float _yRotation;

    [SerializeField]
    private bool _showMouse = false; // need to default to true in "Start" as editor doesnt reflect it otherwise

    private bool _followMouse = true;

    private float _lastMouseX;
    private float _lastMouseY;
    private float _mouseX;
    private float _mouseY;
    
    void Awake()
    {
        MaterialManager.Instance.Init();
    }

    void Start () {
        Console.WriteLine("CameraController.Start");

        _showMouse = true;

        Cursor.visible = _showMouse;  //need to set this here rather than var declaration because editor doesn't notice it

        _xRotation = GetComponent<Camera>().transform.rotation.eulerAngles.x;
        _yRotation = GetComponent<Camera>().transform.rotation.eulerAngles.y;


        _lastMouseX = Input.GetAxis("Mouse X");
        _lastMouseY = -Input.GetAxis("Mouse Y");
        _mouseX = _lastMouseX;
        _mouseY = _lastMouseY;
    }
	
	void Update () {
        if (_followMouse)
        {
            _mouseX = Input.GetAxis("Mouse X");
            _mouseY = -Input.GetAxis("Mouse Y");
            _yRotation += Mathf.Lerp(_lastMouseX * _rotationSpeed, _mouseX * _rotationSpeed, Time.smoothDeltaTime);
            _xRotation += Mathf.Lerp(_lastMouseY * _rotationSpeed, _mouseY * _rotationSpeed, Time.smoothDeltaTime);
            _lastMouseX = _mouseX;
            _lastMouseY = _mouseY;
        }

        ClampCamera();
        GetComponent<Camera>().transform.eulerAngles = new Vector3(_xRotation, _yRotation, 0f);

        if (Input.GetAxis("Vertical") != 0f)
        {
            if (_mode == Mode.Horizontal)
                GetComponent<Camera>().transform.position += Vector3.Cross(GetComponent<Camera>().transform.right, Vector3.up) *
                                             Input.GetAxis("Vertical") *
                                             _movementSpeed * Time.deltaTime;
            else
                GetComponent<Camera>().transform.position += GetComponent<Camera>().transform.forward *
                                             Input.GetAxis("Vertical") *
                                             _movementSpeed * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") != 0f)
            GetComponent<Camera>().transform.position += GetComponent<Camera>().transform.right *
                                         Input.GetAxis("Horizontal") *
                                         _movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            GetComponent<Camera>().transform.position += Vector3.up * _movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.C))
            GetComponent<Camera>().transform.position -= Vector3.up * _movementSpeed * Time.deltaTime;

        if (Input.GetMouseButtonUp(1))
            if (_followMouse)
            {
                _followMouse = false;
            }
            else
            {
                _followMouse = true;
            }


        Cursor.visible = _showMouse;
    }
    
    private void ClampCamera(float horizontal = 360f, float vertical = 80f)
    {
        if (_yRotation < -horizontal) _yRotation += horizontal;
        if (_yRotation > horizontal) _yRotation -= horizontal;
               
        if (_xRotation > vertical) _xRotation = vertical;
        if (_xRotation < -vertical) _xRotation = -vertical;
    }

}
