using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePlateExit : MonoBehaviour
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
            Debug.Log("Exit reverse wonderland");

            isEntered = true;
            keyboardController.GetComponent<KeyboardController>().startController();
            bgm.pitch = 1f;

            if (!haveShowWarning)
            {
                reverseWarning.SetActive(true);
                haveShowWarning = true;
            }
        }
    }
}
