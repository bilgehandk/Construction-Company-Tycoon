using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{

    public Material skyone;

    public Material skytwo;

    public Material skythree;

    public Material skyfour;
    
    // Start is called before the first frame update
    void Start()
    {
        if (DateTime.Now.Hour > 6 && DateTime.Now.Hour <= 12)
        {
            RenderSettings.skybox = skyone;
        }
        else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour <= 18)
        {
            RenderSettings.skybox = skytwo;
        }
        else if (DateTime.Now.Hour > 18 && DateTime.Now.Hour <= 24)
        {
            RenderSettings.skybox = skythree;
        }
        else if(DateTime.Now.Day >= 0 && DateTime.Now.Hour <= 6)
        {
            RenderSettings.skybox = skyfour;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
