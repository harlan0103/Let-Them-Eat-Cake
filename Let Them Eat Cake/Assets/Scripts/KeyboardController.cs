using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [Header("RigidBody")]
    public Rigidbody rb_leftleg;
    public Rigidbody rb_rightleg;

    [Header("FooteCollider")]
    public FootCollider leftCollider;
    public FootCollider rightCollider;

    [Header("Audio")]
    public AudioSource leftMoveAudio;
    public AudioSource leftJumpAudio;
    public AudioSource rightMoveAudio;
    public AudioSource rightJumpAudio;

    [Header("Value")]
    public float leftForce;
    public float rightForce;
    public float jumpForce;
    public float velocityThreshold = 2f;

    public int flag;
    private int i = 0;
    
    // Airconsole control left
    private string directionKey_left;
    public string DirectionKey_left {
        set { directionKey_left = value; }
    }
    private bool isPressed_left = false;
    public bool IsPressed_left {
        set { isPressed_left = value; }
    }

    // Airconsole control right
    private string directionKey_right;
    public string DirectionKey_right {
        set { directionKey_right = value; }
    }
    private bool isPressed_right = false;
    public bool IsPressed_right {
        set { isPressed_right = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        flag = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Left
        if (rb_leftleg != null && rb_leftleg.velocity.magnitude < velocityThreshold)     
        {
            leftLegMovement();
        }

        // Right 
        if (rb_rightleg != null && rb_rightleg.velocity.magnitude < velocityThreshold)    
        {
            rightLegMovement();
        }

        if (Input.GetKeyDown(KeyCode.Q)) leftPlayerJump();
        if (Input.GetKeyDown(KeyCode.O)) rightPlayerJump();
    }

    public void leftPlayerJump()
    {
        if (leftCollider.isColliding)
        {
            HighlightBtn.instance.LeftLegMove(4);
            leftJumpAudio.Play();
            rb_leftleg.AddForce(
                new Vector3(0, jumpForce, jumpForce * 0.5f) * flag , ForceMode.Impulse
            );
            leftCollider.isColliding = false;
        }
       
    }

    public void rightPlayerJump()
    {
        if (rightCollider.isColliding)
        {
            HighlightBtn.instance.RightLegMove(4);
            rightJumpAudio.Play();
            rb_rightleg.AddForce(
                new Vector3(0, jumpForce, jumpForce * 0.5f) * flag, ForceMode.Impulse
            );
            rightCollider.isColliding = false;
        }
    }

    private void leftLegMovement()
    {
        if (Input.GetKey(KeyCode.W) || (directionKey_left == "up" && isPressed_left))
        {
            //Debug.Log("Right foot jump");
            HighlightBtn.instance.LeftLegMove(0);
            leftMoveAudio.Play();
            rb_leftleg.AddForce(
                rb_leftleg.transform.forward * leftForce * flag, ForceMode.Impulse
            );
            
        }

        if (Input.GetKey(KeyCode.S) || (directionKey_left == "down" && isPressed_left))
        {
            HighlightBtn.instance.LeftLegMove(1);
            leftMoveAudio.Play();
            rb_leftleg.AddForce(
                rb_leftleg.transform.forward * -leftForce * flag, ForceMode.Impulse
            );
        }

        if (Input.GetKey(KeyCode.A) || (directionKey_left == "left" && isPressed_left))
        {
            HighlightBtn.instance.LeftLegMove(2);
            leftMoveAudio.Play();
            rb_leftleg.AddForce(
                rb_leftleg.transform.right * 0.7f  * leftForce * flag, ForceMode.Impulse
            );
        }

        if (Input.GetKey(KeyCode.D) || (directionKey_left == "right" && isPressed_left))
        {
            HighlightBtn.instance.LeftLegMove(3);
            leftMoveAudio.Play();leftMoveAudio.Play();
            rb_leftleg.AddForce(
                rb_leftleg.transform.right  * -leftForce * flag, ForceMode.Impulse
            );
        }
    }

    private void rightLegMovement()
    {
        if (Input.GetKey(KeyCode.I) || (directionKey_right == "up" && isPressed_right))
        {
            HighlightBtn.instance.RightLegMove(0);
            rightMoveAudio.Play();
            rb_rightleg.AddForce(
                rb_rightleg.transform.forward * rightForce * flag, ForceMode.Impulse
            );
        }

        if (Input.GetKey(KeyCode.K) || (directionKey_right == "down" && isPressed_right))
        {
            HighlightBtn.instance.RightLegMove(1);
            rightMoveAudio.Play();
            rb_rightleg.AddForce(
                rb_rightleg.transform.forward * -rightForce * flag, ForceMode.Impulse
            );
        }

        if (Input.GetKey(KeyCode.J) || (directionKey_right == "left" && isPressed_right))
        {
            HighlightBtn.instance.RightLegMove(2);
            rightMoveAudio.Play();
            rb_rightleg.AddForce(
                rb_rightleg.transform.right * rightForce * flag, ForceMode.Impulse
            );
        }

        if (Input.GetKey(KeyCode.L) ||  (directionKey_right == "right" && isPressed_right))
        {
            HighlightBtn.instance.RightLegMove(3);
            rightMoveAudio.Play();
            rb_rightleg.AddForce(
                rb_rightleg.transform.right * 0.7f * -rightForce * flag, ForceMode.Impulse
            );
        }
    }

    public void moveDirectionReverse()
    {
        this.flag = -1;
    }

    public void startController()
    {
        this.flag = 1;
    }

    public void stopMovement()
    {
        this.flag = 0;
    }
}
