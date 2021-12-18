using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    private float radian = 0;
    private float radianChange = 0.008f;
    private float radius = 0.8f;
    Vector3 oldPosition; // Position at beginning
    void Start()
    {
        oldPosition = transform.position; // Save the position
    }
    // Update is called once per frame
    void Update()
    {
        radian += radianChange;
        Vector3 yChange = new Vector3(0, Mathf.Cos(radian) * radius, 0);
        transform.position = oldPosition + yChange;
    }
}
