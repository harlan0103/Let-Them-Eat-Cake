using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Camera MainCam;
    public GameObject targetPosition;

    private bool changePos;

    // Start is called before the first frame update
    void Start()
    {
        changePos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changePos)
        {
            MainCam.transform.position = Vector3.Lerp(MainCam.transform.position, targetPosition.transform.position, Time.deltaTime);
        }
        
    }

    public void StartCamMove()
    {
        Debug.Log("isTriggered");
        changePos = true;
    }

}
