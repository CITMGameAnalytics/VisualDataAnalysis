using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsDrawing : MonoBehaviour
{

    public Camera statisticsCamera;
    public Camera playerCamera;
         
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateZenitalView(bool activate)
    {
        if (activate)
        {
            playerCamera.enabled = false;
            statisticsCamera.enabled = true;
        }
        else
        {

            playerCamera.enabled = true;
            statisticsCamera.enabled = false;
        }
    }
}
