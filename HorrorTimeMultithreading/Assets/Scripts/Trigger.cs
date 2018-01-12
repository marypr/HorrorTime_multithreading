using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour {

    public int BoxWidth = 125;
    public int BoxHeight = 25;
    public bool text = false;


    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player"))
        {
            text = true;
        
            OnGUI();
        } }


    void OnGUI() {
        if(text)
            GUI.Box(new Rect((Screen.width / 2), (Screen.height / 2), BoxWidth, BoxHeight), "You win!");
        }
    void Start()
    {

    }


    void Update()
    {
  
        }

    }


