using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreBoard
{
    public List<PlayerScore> highScoreBoardList;

    public HighScoreBoard()
    {
        highScoreBoardList = new List<PlayerScore>();
    }

    public List<PlayerScore> GetHighScreBoardList()
    {
        return highScoreBoardList;
    }

    // Will return the index of new score
    public int AddNewScore(PlayerScore score)
    {
        int place = 0;
        int initialLen = highScoreBoardList.Count;

        if (initialLen > 0)
        {
            foreach (PlayerScore highScore in highScoreBoardList)
            {
                if (score.GetTimeResult() <= highScore.GetTimeResult())
                {
                    highScoreBoardList.Insert(place, score);
                    break;
                }
                place++;
            }

            if (highScoreBoardList.Count == initialLen) // All elements in list are smaller than current one
            {
                highScoreBoardList.Add(score);
            }

            // Now check how many elements are in the list
            if (highScoreBoardList.Count > 10)
            {
                highScoreBoardList.RemoveAt(10);    // Remove the last one and keep list has 10 elements
            }
        }
        else
        {
            // No element in the list
            highScoreBoardList.Add(score);
        }

        return place;
    }

    public void PrintOutScoreBoard()
    {
        Debug.Log("Print out score board");
        int index = 0;
        foreach (PlayerScore score in highScoreBoardList)
        {
            Debug.Log(index + ": " + score.GetTimeResult() + " : " + score.GetPlayerName());
            index++;
        }
    }

}
