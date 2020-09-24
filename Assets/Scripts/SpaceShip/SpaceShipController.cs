using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] private Rigidbody shipRigidbody;
    [SerializeField] private float forwardAcceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float backwardAcceleration;
    [SerializeField] private float turnStrenght;
    [SerializeField] private float maxForwardSpeed;
    [SerializeField] private float maxBackwardSpeed;
    [SerializeField] private GameObject ship;
    [SerializeField] private float life;

    private int accelerate;
    private float turnInput;

    private float velocity;

    private void Update()
    {
        
        accelerate = (int)Input.GetAxisRaw("Vertical");

        turnInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        velocity = Vector3.Dot(shipRigidbody.velocity, transform.forward);
        if (accelerate > 0)
        {
            if (velocity < maxForwardSpeed)
            {
                velocity += forwardAcceleration * Time.deltaTime;
            }
            else
            {
                velocity = maxForwardSpeed;
            }
        }
        else if (accelerate < 0)
        {
            if (velocity > -maxBackwardSpeed)
            {
                velocity -= backwardAcceleration * Time.deltaTime;
            }
            else
            {
                velocity = -maxBackwardSpeed;
            }
        }
        else
        {
            if (velocity > 0)
            {
                velocity -= deceleration * Time.deltaTime;
                if (velocity < 0)
                {
                    velocity = 0;
                }
            }
            else if (velocity < 0)
            {
                velocity += deceleration * Time.deltaTime;
                if (velocity > 0)
                {
                    velocity = 0;
                }
            }

        }

        shipRigidbody.velocity = transform.forward * velocity;

        transform.eulerAngles += new Vector3( 0,turnStrenght * turnInput * Time.deltaTime, 0);
        ship.transform.eulerAngles = new Vector3(-Mathf.Abs(turnInput) * 10, transform.eulerAngles.y, -turnInput * 30);
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (Vector3.Dot(transform.forward, collision.contacts[0].normal) < 0)
        {
            shipRigidbody.velocity /= 10;
        }

        Debug.Log(Vector3.Dot(collision.relativeVelocity, transform.forward));
        //float lifeToRemove = Vector3.
    }*/
}
