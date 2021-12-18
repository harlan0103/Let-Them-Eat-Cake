using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Trigger : MonoBehaviour
{
    public GameObject MMBeanGroup;
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
        if (other.gameObject.tag == "Player" && !isTriggered)
        {
            isTriggered = true;         // Only call this once
            MMBeanGroup.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
    }
}
