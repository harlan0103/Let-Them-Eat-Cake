using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampPlayerName : MonoBehaviour
{
    public Text playerName;

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(transform.position);
        playerName.transform.position = namePos; 
    }
}
