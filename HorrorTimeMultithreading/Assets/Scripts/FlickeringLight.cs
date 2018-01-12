using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
   
    public double minTime ;
    public double maxTime ;
    public bool flickering = true;
    private double timer;
    public Light light = new Light();
    System.Random rand = new System.Random();
    static Mutex mutexObj = new Mutex();

    // Use this for initialization
    void Start()
    {
      //  light = GetComponent<Light>();
       // light.name(FlickeringLight.);
        Flickers();
        timer = GetRandomNumber(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (flickering)
        {
            timer=timer-0.05; 
            if (timer <= 0)
            {
                // light.intensity = 5.0f;
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
                Flickers();
                timer = GetRandomNumber(minTime, maxTime);
            }
        }
    }
    public void Math() {
        mutexObj.WaitOne();
        minTime = GetRandomNumber(0.02, 0.07);
        maxTime = GetRandomNumber(1, 1.5);
          mutexObj.ReleaseMutex();
    }
    public void Flickers()
    {
        Thread newThrd = new Thread((Math));
        newThrd.Start();

    }
    public double GetRandomNumber(double minimum, double maximum)
    {
       
        return rand.NextDouble() * (maximum - minimum) + minimum;
    }
}