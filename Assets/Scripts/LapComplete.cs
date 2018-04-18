using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapComplete : MonoBehaviour {

    public GameObject lapCompleteTrig;
    public GameObject halfPointTrig;

    public Text minuteDisplay;
    public Text secDisplay;
    public Text milliSecDisplay;
    public Text lapCounter;

    private int lapDone = 1;
    public float rawTime;


    private void OnTriggerEnter(Collider other)
    {
        lapDone++;
        rawTime = PlayerPrefs.GetFloat("RawTime");
        if (LapTimeManager.rawTime <= rawTime)
        {
            if (LapTimeManager.secCount <= 9)
                secDisplay.text = "0" + LapTimeManager.secCount + ".";
            else secDisplay.text = "" + LapTimeManager.secCount + ".";

            if (LapTimeManager.minuteCount <= 9) minuteDisplay.text = "0" + LapTimeManager.minuteCount + ":";
            else minuteDisplay.text = "" + LapTimeManager.minuteCount + ":";

            milliSecDisplay.text = "" + LapTimeManager.milliSecCount;
        }
        
        lapCounter.text = "" + lapDone;

        PlayerPrefs.SetInt("MinSave",LapTimeManager.minuteCount);
        PlayerPrefs.SetInt("SecSave", LapTimeManager.secCount);
        PlayerPrefs.SetFloat("MilliSecSave", LapTimeManager.milliSecCount);
        PlayerPrefs.SetFloat("RawTime", LapTimeManager.rawTime);

        LapTimeManager.milliSecCount = 0;
        LapTimeManager.secCount = 0;
        LapTimeManager.minuteCount = 0;

        LapTimeManager.rawTime = 0;
        halfPointTrig.SetActive(true);
        lapCompleteTrig.SetActive(false);


    }
}
