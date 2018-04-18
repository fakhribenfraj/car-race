using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle;
    public float turnSpeed = 5f;
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelBR;
    public WheelCollider wheelBL;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 3f;
    public float minDictanceCarNode = 0.5f;
    public Vector3 centerOfMass;
    public int startNode;
    public bool isBraking = false;
    public Texture2D textureNormal;
    public Texture2D textureBraking;
    public Renderer carRenderer;
    [Header("sensors")]
    public float sensorLength;
    public float sensorLengthSide;
    public Vector3 frontSensorPosition = new Vector3(0f,-0.01f,0.7f);
    public float frontSideSensorPosition;
    public float frontSensorAngle;
    private float targetSteerAngle = 0;

    private List<Transform> nodes;
    private int currentNode;
    private bool avoiding = false;
    //private bool hitBoundry = false;

    // Use this for initialization
    void Start () {
        currentNode = startNode;
        GetComponent<Rigidbody>().centerOfMass = centerOfMass; 
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	void FixedUpdate () {
        Sensors();
        ApplySteer();
        LerpToSteerAngle();
        CheckWayPointDistance();
        Drive();
        Braking();
	}

    void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;
        float avoidMultiplyer = 0;
        avoiding = false;
        //hitBoundry = false;

        //right side sensor
        if (Physics.Raycast(transform.position, transform.right, out hit, sensorLengthSide))
        {
            Debug.DrawLine(transform.position, hit.point,Color.yellow);
        }

        //right side sensor
        if (Physics.Raycast(transform.position, -transform.right, out hit, sensorLengthSide))
        {
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
        }

        //front right sensor
        sensorStartPos += transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("ground"))
            {
               
               avoiding = true;
               avoidMultiplyer -= 0.5f;
               
                
            }
            Debug.DrawLine(sensorStartPos, hit.point);
        }

        //front right angle sensor
       else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle,transform.up) * transform.forward, out hit, sensorLength))
       {
            if (!hit.collider.CompareTag("ground"))
            {
                avoiding = true;
                avoidMultiplyer -= 0.25f;
            }
            Debug.DrawLine(sensorStartPos, hit.point);

        }

        //front left sensor
        sensorStartPos -= 2 * transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("ground"))
            {
                avoiding = true;
                avoidMultiplyer += 0.5f;

            }
            Debug.DrawLine(sensorStartPos, hit.point);

        }

        //front left angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
         {
            if (!hit.collider.CompareTag("ground"))
            {
                avoiding = true;
                avoidMultiplyer += 0.25f;

            }
            Debug.DrawLine(sensorStartPos, hit.point);

        }

        //front center sensor
        if (avoidMultiplyer == 0) { 
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("ground"))
                {
                    avoiding = true;
                    if(hit.normal.x < 0)
                    {
                        avoidMultiplyer = -1;
                    }
                    else
                    {
                        avoidMultiplyer = 1;
                    }

                }
                Debug.DrawLine(sensorStartPos, hit.point);


            }
        }

        if (avoiding)
        {
            targetSteerAngle = maxSteerAngle * avoidMultiplyer;
           /* wheelFL.steerAngle = maxSteerAngle * avoidMultiplyer;
            wheelFR.steerAngle = maxSteerAngle * avoidMultiplyer;*/
        }

    }

    void ApplySteer()
    {
        if (avoiding)
            return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetSteerAngle = newSteer;
        /*wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;*/
    }
    void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if(currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelBL.motorTorque = maxMotorTorque;
            wheelBR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelBL.motorTorque = 0;
            wheelBR.motorTorque = 0;
        }
    }
    void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < minDictanceCarNode)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
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
    void LerpToSteerAngle()
    {
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);

    }
}
