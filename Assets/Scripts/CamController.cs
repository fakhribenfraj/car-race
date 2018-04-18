using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {
    public GameObject[] cars;

    private GameObject targetCar;
   // private Vector3 offset;
    private bool firstDetect = true;
	
	// Update is called once per frame
	void Update () {
        if (firstDetect) { 
            for (int i = 0; i < cars.Length; i++)
                if (cars[i].GetComponent<PlayerController>().isPlayer && firstDetect)
                {
                    targetCar = cars[i];
                    //offset =transform.position - targetCar.transform.position;
                    firstDetect = false;
                    break;
                }
        }


        transform.eulerAngles = new Vector3(transform.eulerAngles.x
            , targetCar.transform.eulerAngles.y
            , transform.eulerAngles.z);

        /*transform.position = new Vector3(targetCar.transform.position.x + offset.x
            , targetCar.transform.position.y + offset.y
            , targetCar.transform.position.z + offset.z);*/
	}
}
