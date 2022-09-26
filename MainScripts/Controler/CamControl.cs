using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float fov;
    [SerializeField] private float InvertX;
    [SerializeField] private float InvertY;
    [SerializeField] public bool camDisabled = false;
    public Transform playerModel;
    float xAxisRoto = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookSensitivity = SettingsMenu.mouseSensitivity;
        fov = SettingsMenu.camFov;
        Camera.main.fieldOfView = fov;
        InvertX = SettingsMenu.InvertXValue;
        InvertY = SettingsMenu.InvertYValue;
    }
    void Update()
    {
        if (!camDisabled)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * InvertX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * InvertY *Time.deltaTime;

            xAxisRoto -= mouseY;
            xAxisRoto = Mathf.Clamp(xAxisRoto, -90f, 90f);
       
            transform.localRotation = Quaternion.Euler(xAxisRoto, 0f, 0f);

            playerModel.Rotate(Vector3.up * mouseX);
        }
    }
    public void FovChange()
    {
        fov = SettingsMenu.camFov;
        Camera.main.fieldOfView = fov;
    }
}
