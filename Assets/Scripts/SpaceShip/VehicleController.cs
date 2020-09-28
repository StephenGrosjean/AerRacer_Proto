using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    private float speed;

    [Header("Vehicle Movement Settings")] 
    [SerializeField] private float forwardForce;
    [SerializeField] private float rotationForce;
    [SerializeField] private float backwardForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float vehicleLeaningSideAngleMultiplier;
    [SerializeField] private float vehicleLeaningBackwardsAngleMultiplier;

    [Header("Hover Settings")] 
    [SerializeField] private float hoverHeight;

    [Header("Vehicle Components")] 
    [SerializeField] private Transform shipTransform;
    [SerializeField] private Transform shipModelTransform;
    [SerializeField] private Rigidbody shipRigidbody;
    [SerializeField] private PlayerInput playerInput;

    [Header("Physics Settings")] 
    [SerializeField] private float maximumForwardSpeed;
    [SerializeField] private float maximumBackwardSpeed;
    [SerializeField] private float maximumAngularSpeed;

    void FixedUpdate()
    {
        speed = Vector3.Dot(shipRigidbody.velocity, shipTransform.forward);

        VehicleHover();
        VehicleMovements();
    }

    void Update()
    {
        shipModelTransform.localEulerAngles = new Vector3(speed * Mathf.Abs(shipRigidbody.angularVelocity.y) * -vehicleLeaningBackwardsAngleMultiplier, shipModelTransform.localEulerAngles.y, -shipRigidbody.angularVelocity.y * vehicleLeaningSideAngleMultiplier);
    }

    void VehicleHover()
    {

    }

    void VehicleMovements()
    {
        float rotationTorque = playerInput.vehicleRotationForceInput * rotationForce /*- shipRigidbody.angularVelocity.y*/;

        shipRigidbody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);

        float angularSpeed = shipRigidbody.angularVelocity.y;

        Debug.Log(angularSpeed);

        if (Mathf.Abs(angularSpeed) > maximumAngularSpeed)
        {
            if (angularSpeed > 0)
            {
                shipRigidbody.angularVelocity = new Vector3(shipRigidbody.angularVelocity.x, maximumAngularSpeed, shipRigidbody.angularVelocity.z);
            }
            else
            {
                shipRigidbody.angularVelocity = new Vector3(shipRigidbody.angularVelocity.x, -maximumAngularSpeed, shipRigidbody.angularVelocity.z);
            }
        }

        float sidewaysSpeed = Vector3.Dot(shipRigidbody.velocity, transform.right);

        Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.deltaTime);

        shipRigidbody.AddForce(sideFriction, ForceMode.Acceleration);

        if (playerInput.vehiclePowerForceInput > 0)
        {
            if (speed < 0)
            {
                shipRigidbody.AddForce(transform.forward * brakeForce, ForceMode.Acceleration);
                if (Vector3.Dot(shipRigidbody.velocity, shipTransform.forward) > 0)
                {
                    shipRigidbody.velocity = transform.forward * 0;
                }
            }
            else
            {
                shipRigidbody.AddForce(transform.forward * forwardForce, ForceMode.Acceleration);
                if (Vector3.Dot(shipRigidbody.velocity, shipTransform.forward) > maximumForwardSpeed)
                {
                    shipRigidbody.velocity = transform.forward * maximumForwardSpeed;
                }
            }
        }
        else if (playerInput.vehiclePowerForceInput < 0)
        {
            if (speed > 0)
            {
                shipRigidbody.AddForce(transform.forward * -brakeForce, ForceMode.Acceleration);
                if (Vector3.Dot(shipRigidbody.velocity, shipTransform.forward) < 0)
                {
                    shipRigidbody.velocity = transform.forward * 0;
                }
            }
            else
            {
                shipRigidbody.AddForce(transform.forward * -backwardForce, ForceMode.Acceleration);
                if (Vector3.Dot(shipRigidbody.velocity, shipTransform.forward) < -maximumBackwardSpeed)
                {
                    shipRigidbody.velocity = transform.forward * -maximumBackwardSpeed;
                }
            }
        }
        else
        {
            
        }
    }


}
