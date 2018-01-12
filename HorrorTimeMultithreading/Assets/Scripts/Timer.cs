using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    public float timeRemaining = 60f;
    public int BoxWidth = 125;
    public int BoxHeight = 25;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeRemaining > 0)
        {
    
            timeRemaining -= Time.deltaTime;
            if (timeRemaining == 0) { GameOver(); }
        }

    }
    void GameOver()
    {

    }
    void OnGUI() {

        GUI.Box(new Rect(Screen.width - BoxWidth, 0, BoxWidth, BoxHeight), "The end in " + Math.Round(timeRemaining, 0) + " sec");
    }
}
