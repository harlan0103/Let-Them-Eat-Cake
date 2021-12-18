using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToFinalCamera : MonoBehaviour
{
    public Camera defaultCam;
    public Camera finalCam;
    
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            defaultCam.enabled = false;
            finalCam.enabled = true;
        }
    }
}
