using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmFasterTrigger : MonoBehaviour
{
    public GameObject bgmObject;
    public AudioClip fasterBgm;
    public KeyboardController keyboardController;
    private bool isEnter = false;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !isEnter)
        {
            Debug.Log("Exit");
            AudioSource bgmSource = bgmObject.GetComponent<AudioSource>();
            bgmSource.clip = fasterBgm;
            bgmSource.Play();
            bgmSource.pitch = 1f;
            keyboardController.GetComponent<KeyboardController>().startController();
            isEnter = true;
        }
    }


}
