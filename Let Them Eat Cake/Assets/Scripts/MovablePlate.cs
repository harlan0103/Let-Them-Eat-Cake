using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlate : MonoBehaviour
{
    public Transform initialPosition;
    public Transform targetPosition;

    public GameObject player;

    private int direction;
    private bool coroutineEnd;
    private bool isPlayerEnterPlate;

    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        coroutineEnd = false;
        isPlayerEnterPlate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!coroutineEnd)
        {
            StartCoroutine(moveCoroutine(direction));        
        }
    }

    IEnumerator moveCoroutine(int direction)
    {
        coroutineEnd = true;
        yield return new WaitForSeconds(3f);    // First wait for 3 seconds for player to get to the plate

        if (direction == 1)
        {
            while (Vector3.Distance(transform.position, targetPosition.position) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime);
                yield return null;
            }
        }
        else if (direction == -1)
        {
            while (Vector3.Distance(transform.position, initialPosition.position) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, initialPosition.position, Time.deltaTime);
                yield return null;
            }
        }

        changeDirection();

        coroutineEnd = false;
        yield return new WaitForSeconds(3f);
    }

    private void changeDirection()
    {
        direction *= -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("enter collision");
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("exit collision");
            player.transform.parent = null;
        }
    }
}
