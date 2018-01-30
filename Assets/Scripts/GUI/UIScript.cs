using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private Text scoreCounter;
    private void Start()
    {
        scoreCounter = GameObject.Find("Score").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {
		if (Globals.tileCounter < 10)
        {
            scoreCounter.text = "0" + Globals.tileCounter;
        }
        else
        {
            scoreCounter.text = Globals.tileCounter.ToString();
        }
	}
}
