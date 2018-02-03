using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

    private Text scoreCounter;

    private void Start()
    {
        scoreCounter = GameObject.Find("Score").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Globals.GetInstance()._tileCounter < 10)
        {
            scoreCounter.text = "0" + Globals.GetInstance()._tileCounter;
        }
        else
        {
            scoreCounter.text = Globals.GetInstance()._tileCounter.ToString();
        }
	}
}
