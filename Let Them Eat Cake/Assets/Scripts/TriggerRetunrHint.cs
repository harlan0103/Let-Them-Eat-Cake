using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRetunrHint : MonoBehaviour
{
    public GameObject returnHint;
    private bool haveShowHint;
    
    // Start is called before the first frame update
    void Start()
    {
        haveShowHint = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showRetunrHint()
    {
        if (!haveShowHint)
        {
            returnHint.SetActive(true);
            haveShowHint = true;
        }
    }
}
