using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScores : MonoBehaviour
{
    User user;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject scorePanel;

    void Start()
    {
        scorePanel.gameObject.SetActive(false);
        user = GameObject.Find("UserDataObject").GetComponent<User>();
    }
    public void ShowScore()
    {
        if (!scorePanel.gameObject.activeSelf)
        {
            scorePanel.gameObject.SetActive(true);
        }
        else
        {
            scorePanel.gameObject.SetActive(false);
        }

        makeScores();
    }

    public void makeScores()
    {
        int minutes = (int)(user.GetTotalTime() / 60.0f);
        int hours = minutes / 60;
        minutes = minutes % 60;
        int seconds = (int)(user.GetTotalTime() % 60.0f);
        scoreText.text = $"HighScore1: {user.GetScoreOne(0)} \n HighScore2: {user.GetScoreOne(1)} \n HighScore3: {user.GetScoreOne(2)} \n \n TotalTime: {hours}:{minutes}:{seconds} \n PlayerTag: {user.GetPlayerTagCount()}";
    }

}
