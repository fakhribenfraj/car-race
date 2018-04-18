using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    public Text countDownTxt;
    public AudioSource getReady;
    public AudioSource goAudio;
    public GameObject lapTimer;
    public GameObject carControls;
    public AudioSource levelMusic;


    // Use this for initialization
    void Start () {
        StartCoroutine(CountStart());
	}

    private IEnumerator CountStart ()
    {
        yield return new WaitForSeconds(0.5f);
        countDownTxt.text = "3";
        getReady.Play();
        countDownTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        countDownTxt.gameObject.SetActive(false);
        countDownTxt.text = "2";
        getReady.Play();
        countDownTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        countDownTxt.gameObject.SetActive(false);
        countDownTxt.text = "1";
        getReady.Play();
        countDownTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        countDownTxt.gameObject.SetActive(false);
        countDownTxt.text = "GO!!";
        goAudio.Play();
        levelMusic.Play();
        countDownTxt.gameObject.SetActive(true);
        lapTimer.SetActive(true);
        carControls.SetActive(true);
    }
}
