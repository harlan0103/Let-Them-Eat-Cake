using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

using System.IO;
using System.Xml.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Test")]
    public bool startDirectly = false;
    public Transform startPosition;
    public GameObject character;
    public GameObject[] startUI;
    
    [Header("GameObjet")]
    public static GameManager instance;
    public GameObject connectHint; 
    public GameObject finishTextObject;
    public KeyboardController charController;
    public GameObject startAnimation;
    public GameObject introCamera;

    [Header("Text")]
    public Text countdown;
    public Text timer;
    public Text timeResultText;
    public Text gameTitle;

    [Header("Audio")]
    public AudioSource countDownAudioSource;
    public AudioSource crowdCheering;

    [Header("Value")]
    public int countdownTime;

    [Header("High Score Board Entries")]
    public Text[] playerNameArray;
    public Text[] timeArray;
    public Text unrankedName;
    public Text unrankedTime;
    public GameObject highScoreBoardUI;
    
    [Header("Score Board")]
    public GameObject scoreBoardUI;
    public Text sb_time;
    public Text sb_playerName;

    [Header("Show high score board or just score")]
    public bool showHighScoreBoard = true;

    public string playerName;               // Fake name for testing

    // Control background music
    private AudioSource bgm;
    private float defaultVolume;

    private bool isGameActive;              // Game status; false when player finishes the game
    private float gameTime;
    private float waitTime;

    private float timeResult;
    private HighScoreBoard highScoreBoard;  // Class contains a sorted list of players high score
    private PlayerScore currentScore;       // Class contains current player result

    [Header("Button")]
    public GameObject controlBtn;
    
    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(playerName);

        bgm = GetComponent<AudioSource>();
        defaultVolume = bgm.volume;

        isGameActive = false;

        currentScore = new PlayerScore();

        //finishTextObject.SetActive(false);

        // Load high score board here
        LoadHighScoreBoard();

        // Hide unranked name and time
        unrankedName.gameObject.SetActive(false);
        unrankedTime.gameObject.SetActive(false);

        controlBtn.SetActive(false);



        if (startDirectly) 
        {
            charController.flag = 1;
            startAnimation.SetActive(false);
            foreach (var ui in startUI) ui.SetActive(false);
            character.transform.position = startPosition.position;
            controlBtn.SetActive(true);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectComplete()
    {
        gameTitle.gameObject.SetActive(false);
        Text text = connectHint.GetComponentInChildren<Text>();
        text.text = "Connect Complete!";
        Animator animation = connectHint.GetComponent<Animator>();
        animation.SetTrigger("ToStart");
        startAnimation.SetActive(false);
        introCamera.SetActive(true);
    }

    public void StartCountDown()
    {
        introCamera.SetActive(false);
        countdown.text = "3";
        timer.text = "00:00:00";
        StartCoroutine(CountdownCoroutine());
    }

    // Countdown before game start
    IEnumerator CountdownCoroutine()
    {
        // Show control btns
        controlBtn.SetActive(true);

        // start countdown animation and audio
        countdown.GetComponent<Animator>().enabled = true;
        bgm.volume *= 0.5f;
        countDownAudioSource.Play();

        while (countdownTime > 0)
        {
            countdown.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdown.text = "";
        yield return new WaitForSeconds(0.2f);

        // change "go" style
        countdown.GetComponent<Animator>().enabled = false;
        Color textColor = countdown.color;
        textColor.a = 1;
        countdown.color = textColor;
        countdown.text = "GO!";

        yield return new WaitForSeconds(1f);

        // Game begin
        bgm.volume = defaultVolume;
        charController.startController();
     
        countdown.gameObject.SetActive(false);

        isGameActive = true;

        waitTime = Time.time;

        StartCoroutine(TimerCoroutine());

        yield return null;
    }

    // Game timer on upper left
    IEnumerator TimerCoroutine()
    {
        string tempTime = "";

        while (isGameActive)
        {
            gameTime = Time.time - waitTime;

            //Debug.Log(gameTime);

            tempTime = ConvertTimeFormat(gameTime);

            timer.text = tempTime;
            yield return null;
        }

        crowdCheering.Play();         // Audio play temp

        // When game ends
        timeResult = gameTime;

        controlBtn.SetActive(false);

        finishTextObject.SetActive(true);

        timeResultText.text = timer.text;

        timer.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        finishTextObject.SetActive(false);

        crowdCheering.Stop();         // Audio play temp

        ShowPlayerResult();
    }

    private void ShowPlayerResult()
    {
        //highScoreText.gameObject.SetActive(true);     // For testing
        highScoreBoardUI.SetActive(true);

        // Update player result
        currentScore.SetTimeResult(timeResult);
        currentScore.SetPlayerName(playerName);

        int placement = highScoreBoard.AddNewScore(currentScore);
        //Debug.Log("Rank is: " + placement);

        // Use PlayerPrefs to save data
        SaveHighScoreBoard();

        if (showHighScoreBoard == true)
        {
            ShowHighScoreBoard();
        }
        else
        {
            ShowPlayerScore();
        }
    }

    // This function is called by FinishLine script when player enters finish line
    public void EnterFinishLine()
    {
        // Stop timer
        isGameActive = false;
        charController.stopMovement();      // When player enter the finish line, stop the movement
    }

    // Load high score
    private void LoadHighScoreBoard()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HighScoreBoard));
        string highScore = PlayerPrefs.GetString("highScore");

        if (highScore.Length == 0)
        {
            // If there has no high score yet, create a new one
            highScoreBoard = new HighScoreBoard();
        }
        else
        {
            using (var reader = new StringReader(highScore))
            {
                // Get the highScoreBoard object
                highScoreBoard = serializer.Deserialize(reader) as HighScoreBoard;
            }
        }
    }

    // Store highScoreBoard into PlayerPrefs
    private void SaveHighScoreBoard()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HighScoreBoard));

        using (StringWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, highScoreBoard);
            PlayerPrefs.SetString("highScore", writer.ToString());

            PlayerPrefs.Save();     // Not sure if need to add in WebGL
        }
    }

    private void ShowHighScoreBoard()
    {
        // Show highScore board
        bool isRanked = false;

        List<PlayerScore> list = highScoreBoard.GetHighScreBoardList();
        int rank = 0;

        foreach (PlayerScore score in list)
        {
            playerNameArray[rank].text = score.GetPlayerName();
            timeArray[rank].text = ConvertTimeFormat(score.GetTimeResult());

            if (score.GetTimeResult() == currentScore.GetTimeResult() && score.GetPlayerName().Equals(currentScore.GetPlayerName()))
            {
                isRanked = true;

                // Set current player socre color
                playerNameArray[rank].color = Color.red;
                timeArray[rank].color = Color.red;
            }

            //string resultEntry = "\n" + (rank + 1) + " : " + score.GetTimeResult() + " : " + score.GetPlayerName();
            //highScoreText.text += resultEntry;

            rank++;
        }

        // Show unrecored entry
        if (rank < 10)
        {
            for (int i = rank; i < 10; i++)
            {
                playerNameArray[i].text = "  -- -- -- -- ";
                timeArray[i].text = "-- : -- : --";
            }
        }

        // Show if player is unreanked
        if (!isRanked)
        {
            unrankedName.gameObject.SetActive(true);
            unrankedTime.gameObject.SetActive(true);

            //highScoreText.text += "Unranked : " + currentScore.GetTimeResult() + " : " + currentScore.GetPlayerName() + "\n";
            unrankedName.text = currentScore.GetPlayerName();
            unrankedTime.text = ConvertTimeFormat(currentScore.GetTimeResult());

            unrankedName.color = Color.red;
            unrankedTime.color = Color.red;
        }
    }

    private void ShowPlayerScore()
    {
        scoreBoardUI.SetActive(true);
        sb_playerName.text = currentScore.GetPlayerName();
        sb_time.text = ConvertTimeFormat(currentScore.GetTimeResult());
    }

    private string ConvertTimeFormat(float timeInFloat)
    {
        string timeText = "";

        string minutes = ((int)timeInFloat / 60).ToString();

        if (minutes.Length == 1)    // Set 1:00:00 to 01:00:00
        {
            minutes = "0" + minutes;
        }

        string seconds = (timeInFloat % 60).ToString("f2");

        //string secondsRev = seconds.Substring(0,2);

        string secondsRev;
        string ms;

        if (seconds.Length == 4)
        {
            secondsRev = seconds.Substring(0, 1);
            secondsRev = "0" + secondsRev;

            ms = seconds.Substring(2, 2);
        }
        else
        {
            secondsRev = seconds.Substring(0, 2);
            ms = seconds.Substring(3, 2);
        }

        timeText = minutes + ":" + secondsRev + ":" + ms;

        return timeText;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log(playerName);
    }
}
