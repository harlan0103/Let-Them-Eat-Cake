using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveTrigger : MonoBehaviour
{
    public CamMovement camMove;
    private bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
            camMove.StartCamMove();
        }
    }
}
