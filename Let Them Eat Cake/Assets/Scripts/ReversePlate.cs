using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePlate : MonoBehaviour
{
    public GameObject keyboardController;
    public GameObject reverseWarning;
    public AudioSource bgm;
    
    public bool isEntered;
    private bool haveShowWarning;

    // Start is called before the first frame update
    void Start()
    {
        haveShowWarning = false;
        isEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        When character enters the trigger
        Reverse its moving direction
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !isEntered)
        {
            Debug.Log("Enter trigger");

            keyboardController.GetComponent<KeyboardController>().moveDirectionReverse();
            isEntered = true;
            bgm.pitch = 0.7f;
            
            if (!haveShowWarning)
            {
                reverseWarning.SetActive(true);
                haveShowWarning = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" && isEntered)
        {
            isEntered = false;
            keyboardController.GetComponent<KeyboardController>().startController();
            bgm.pitch = 1f;

        }

    }
}
