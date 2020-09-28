using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string verticalAxisName = "Vertical";
    [SerializeField] private string horizontalAxisName = "Horizontal";
    [SerializeField] private string quitButtonName = "Cancel";

    [HideInInspector] public float vehiclePowerForceInput;
    [HideInInspector] public float vehicleRotationForceInput;

    void Update()
    {
        if (Input.GetButtonDown(quitButtonName))
        {
            Application.Quit();
        }

        vehiclePowerForceInput = Input.GetAxis(verticalAxisName);
        vehicleRotationForceInput = Input.GetAxis(horizontalAxisName);
    }
}
