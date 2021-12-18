using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRecordPoint : MonoBehaviour
{
    public CameraFollowChar CameraFollowChar;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("RecordPoint"))
        {
            CameraFollowChar.RecordPosition(other.transform.position);
            TriggerRetunrHint returnHint = other.gameObject.GetComponent<TriggerRetunrHint>();
            if (returnHint != null ) returnHint.showRetunrHint();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
