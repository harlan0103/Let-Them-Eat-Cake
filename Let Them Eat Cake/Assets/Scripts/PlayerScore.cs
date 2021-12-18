[System.Serializable]
public class PlayerScore
{
    public float timeResult;
    public string playerName;

    public PlayerScore()
    {
        timeResult = 0f;
        playerName = "";
    }

    public void SetTimeResult(float _timeResult)
    {
        timeResult = _timeResult;
    }

    public void SetPlayerName(string _playerName)
    {
        playerName = _playerName;
    }

    public float GetTimeResult()
    {
        return timeResult;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
