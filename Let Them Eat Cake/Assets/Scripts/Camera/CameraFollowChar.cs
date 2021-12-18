using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowChar : MonoBehaviour
{
    
    public Transform followTarget;
    public GameObject center;

    private Vector3 offset;
    private Vector3 charRecordPos;
    private Vector3 camRecordPos;

    // Start is called before the first frame update
    void Start()
    {
        offset = gameObject.transform.position - center.transform.position;
    }

    void Update()
    {
        if (followTarget.position.y < -0.5f)
        {
            ResetCharacterPosition();
            center.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = center.transform.position + offset;
        transform.position = targetPos;
    }

    public void RecordPosition(Vector3 pos)
    {
        charRecordPos = pos;
        camRecordPos = pos + offset;
    }

    public void ResetCharacterPosition()
    {
        followTarget.position = charRecordPos + Vector3.up * 4f;
        transform.position = camRecordPos;
    }
}
