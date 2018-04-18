using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfComplete : MonoBehaviour {

    public GameObject lapCompleteTrig;
    public GameObject halfPointTrig;

    private void OnTriggerEnter(Collider other)
    {
        halfPointTrig.SetActive(false);
        lapCompleteTrig.SetActive(true);
    }
}
