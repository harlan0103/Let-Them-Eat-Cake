using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepCenter : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 defaultPos;
    private Quaternion defaultRot;
    
    void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.localRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = defaultPos;
        transform.localRotation = defaultRot;
    }
}
