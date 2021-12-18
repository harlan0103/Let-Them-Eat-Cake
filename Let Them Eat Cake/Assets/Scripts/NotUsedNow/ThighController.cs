using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThighController : MonoBehaviour
{
    public Rigidbody rb_leftleg;
    public Rigidbody rb_rightleg;
    public float leftForce = 1f;
    public float rightForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ControlLeftLeg(float x, float y)
    {
        Vector3 moveDirection = new Vector3(x, 0f, -y).normalized * leftForce;
        if (rb_leftleg != null) rb_leftleg.AddForce(moveDirection, ForceMode.Impulse);
        Debug.Log("Left");
    }

    public void ControlRightLeg(float x, float y)
    {
        Vector3 moveDirection = new Vector3(x, 0f, -y).normalized * rightForce;
        if (rb_rightleg != null) rb_rightleg.AddForce(moveDirection, ForceMode.Impulse);
        Debug.Log("Right");
    }
}
