using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelBR;
    public WheelCollider wheelBL;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 3f;
    public float maxSteerAngle = 30f;
    public Texture2D textureNormal;
    public Texture2D textureBraking;
    public Renderer carRenderer;
    public bool isBraking = false;
    public bool isPlayer = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Drive();
        if (Input.GetKey(KeyCode.Space))
            isBraking = true;
        else
            isBraking = false;
        Braking();
    }

    void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && !isBraking && Input.GetAxis("Vertical")>0)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelBL.motorTorque = maxMotorTorque;
            wheelBR.motorTorque = maxMotorTorque;
        }
        else if (currentSpeed < maxSpeed && !isBraking && Input.GetAxis("Vertical") < 0)
        {
            wheelFL.motorTorque = -maxMotorTorque;
            wheelFR.motorTorque = -maxMotorTorque;
            wheelBL.motorTorque = -maxMotorTorque;
            wheelBR.motorTorque = -maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelBL.motorTorque = 0;
            wheelBR.motorTorque = 0;
        }
        wheelFL.steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
        wheelFR.steerAngle = maxSteerAngle * Input.GetAxis("Horizontal");
    }

    void Braking()
    {
        if (isBraking)
        {
            carRenderer.material.mainTexture = textureBraking;
            wheelBL.brakeTorque = maxBrakeTorque;
            wheelBR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            carRenderer.material.mainTexture = textureNormal;
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }
    }
}
