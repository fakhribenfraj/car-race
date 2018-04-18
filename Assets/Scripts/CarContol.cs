using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarContol : MonoBehaviour {

    public GameObject[] cars;

	void Start () {
		for(int i = 0; i < cars.Length; i++)
        {
            if (cars[i].GetComponent<PlayerController>().isPlayer)
            {
                cars[i].GetComponent<CarEngine>().enabled = false;
                cars[i].GetComponent<PlayerController>().enabled = true;
            }
            else
            {
                cars[i].GetComponent<CarEngine>().enabled = true;
                cars[i].GetComponent<PlayerController>().enabled = false;
            }
            
        }
	}

}
