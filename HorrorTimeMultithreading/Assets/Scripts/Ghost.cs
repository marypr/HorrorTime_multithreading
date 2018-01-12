using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;


public class Ghost : MonoBehaviour {

    AutoResetEvent resetEvent;
    Thread thread;
    bool goingDown;
    Transform t;
    Vector3 latestScale;
    float x;

    bool stop;

	void Start () {
        x = latestScale.x;
        t = transform;
        latestScale = t.localPosition;
        latestScale.y = 2f;
        thread = new Thread(Run);
        thread.Start();
        resetEvent = new AutoResetEvent(false);


    }

    void Run()
    {
        while(!stop)
        {
            resetEvent.WaitOne();

           x += latestScale.x * (goingDown ? -.01f : .01f);
            latestScale.x = x;

            if ((goingDown && latestScale.x < 1) || (!goingDown && latestScale.magnitude > 8))
            {
                goingDown = !goingDown;
           
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
       
        t.localPosition = latestScale;
      
    }
    void FixedUpdate()
    {

        resetEvent.Set();
    }

    public void OnApplicationQuit()
    {

        stop = true;
        thread.Abort();
    }

    public void OnDestroy()
    {

        stop = true;
        thread.Abort();
    }
}
