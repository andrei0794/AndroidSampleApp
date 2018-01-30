using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private Text scoreCounter;
    private void Start()
    {
        scoreCounter = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {
		if (Globals._tileCounter < 10)
        {
            scoreCounter.text = "0" + Globals._tileCounter;
        }
        else
        {
            scoreCounter.text = Globals._tileCounter.ToString();
        }
	}
}
