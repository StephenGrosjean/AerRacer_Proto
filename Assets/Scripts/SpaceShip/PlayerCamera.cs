using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Components")]
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 cameraLocalPosition;
    [SerializeField] private Vector3 cameraLocalRotation;

    [Header("Vehicle Components")] 
    [SerializeField] private Vector3 vehiclePosition;
    [SerializeField] private Vector3 vehicleRotation;

    private void Update()
    {
        //camera.transform.localPosition = vehiclePosition;
    }

}
