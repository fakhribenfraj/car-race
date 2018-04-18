using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour {

    public static int minuteCount;
    public static int secCount;
    public static float milliSecCount;

    public Text minuteDisplay;
    public Text secDisplay;
    public Text milliSecDisplay;

    public static float rawTime;

    // Update is called once per frame
    void Update () {
        milliSecCount += Time.deltaTime * 10;
        rawTime += Time.deltaTime;
        milliSecDisplay.text = "" + milliSecCount.ToString("F0");
        if(milliSecCount >= 10)
        {
            milliSecCount = 0;
            secCount++;
        }
        if (secCount <= 9) secDisplay.text = "0" + secCount + "."; 
        else secDisplay.text = "" + secCount + ".";

        if (secCount >= 60)
        {
            secCount = 0;
            minuteCount++;
        }
        if (minuteCount <= 9) minuteDisplay.text = "0" + minuteCount + ":";
        else minuteDisplay.text = "" + minuteCount + ":";
    }
}
