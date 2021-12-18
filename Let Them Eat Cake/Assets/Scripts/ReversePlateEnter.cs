using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePlateEnter : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !isEntered)
        {
            Debug.Log("Enter reverse wonderland");

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
}
