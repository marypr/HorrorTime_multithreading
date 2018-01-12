using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }

    }
    

}
