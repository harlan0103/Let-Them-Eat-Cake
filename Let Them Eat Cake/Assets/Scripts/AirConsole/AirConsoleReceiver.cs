using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class AirConsoleReceiver : MonoBehaviour
{
    public KeyboardController keyboardController;
    public Text player1Name;
    public Text player2Name;
    public Text leftPlayerName;
    public Text rightPlayerName;


    // player 0 - left, player 1 - right
    private Dictionary<int, int> deviceIdToPlayer;
    private int playerNum = 0;
    private int pressStartNum = 0;
    private AudioSource audioSource;

    

    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    private void Start() 
    {
        deviceIdToPlayer = new Dictionary<int, int>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMessage(int fromDeviceID, JToken data)
    {
        Debug.Log("Message from: " + fromDeviceID);

        // Receive player input name
        if (data["playerName"] != null && !deviceIdToPlayer.ContainsKey(fromDeviceID) && playerNum < 2)
        {
            int legId = playerNum % 2;
            playerNum ++;
            audioSource.Play();
            deviceIdToPlayer.Add(fromDeviceID, legId);

            Debug.Log(data["playerName"].ToString());

            // Set the player name in the unity
            string name = data["playerName"].ToString();
            if (legId == 0) {
                leftPlayerName.text = name;
                player1Name.text = name;
            } 
            else 
            {
                rightPlayerName.text = name;
                player2Name.text = name;
            }


            if (playerNum == 2) 
            {
                foreach(KeyValuePair<int, int> entry in deviceIdToPlayer)
                {
                    AirConsole.instance.Message(entry.Key, new {
                        connect = entry.Value == 0 ? "Left Leg" : "Right Leg"
                    });
                }
                
                GameManager.instance.ConnectComplete();
            }
        }

        // if (data["joystick_left"] != null && data["joystick_left"]["position"] != null)
        // {
        //     float x = float.Parse(data["joystick_left"]["position"]["x"].ToString());
        //     float y = float.Parse(data["joystick_left"]["position"]["y"].ToString());
        //     if (deviceIdToPlayer[fromDeviceID] == 0) thighController.ControlLeftLeg(x, y);
        //     else thighController.ControlRightLeg(x, y);
        // }
        // Receive the movement

        if (data["start"] != null)
        {
            pressStartNum ++;
            audioSource.Play();
            if (pressStartNum == 2) 
            {
                GameManager.instance.StartCountDown();
                leftPlayerName.enabled = true;
                rightPlayerName.enabled = true;

                // Set player name
                string playerName = leftPlayerName.text + " & " + rightPlayerName.text;
                GameManager.instance.SetPlayerName(playerName);
            }
        }

        if (data["dpad2"] != null && data["dpad2"]["directionchange"] != null)
        {
            string isPressed =  data["dpad2"]["directionchange"]["pressed"].ToString();
            string directionKey = data["dpad2"]["directionchange"]["key"].ToString();
            if (deviceIdToPlayer[fromDeviceID] == 0)
            {
                keyboardController.IsPressed_left = isPressed == "True" ? true : false;
                keyboardController.DirectionKey_left = directionKey;
            } else
            {
                keyboardController.IsPressed_right = isPressed == "True" ? true : false;
                keyboardController.DirectionKey_right = directionKey;

            }
        }

        if (data["jump"] != null)
        {
            if (deviceIdToPlayer[fromDeviceID] == 0)  keyboardController.leftPlayerJump();
            else keyboardController.rightPlayerJump();
        }
    }

    private void OnDestroy()
    {
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}
