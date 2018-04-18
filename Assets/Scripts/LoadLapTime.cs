using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLapTime : MonoBehaviour {

    public int minCount;
    public int secCount;
    public float milliSecCount;
    public Text minDisplay;
    public Text secDisplay;
    public Text milliSecDisplay;

    // Use this for initialization
    void Start () {
        minCount = PlayerPrefs.GetInt("MinSave");
        secCount = PlayerPrefs.GetInt("SecSave");
        milliSecCount = PlayerPrefs.GetFloat("MilliSecSave");

        /*minDisplay.text = "" + minCount+":";
        secDisplay.text = "" + secCount + ".";
        milliSecDisplay.text = "" + milliSecCount.ToString("F0");*/
        if (secCount <= 9)
            secDisplay.text = "0" + secCount + ".";
        else secDisplay.text = "" + secCount + ".";

        if (minCount <= 9) minDisplay.text = "0" + minCount + ":";
        else minDisplay.text = "" + minCount + ":";

        milliSecDisplay.text = "" + milliSecCount.ToString("F0");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
