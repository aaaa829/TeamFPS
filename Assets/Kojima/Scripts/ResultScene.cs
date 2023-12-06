using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    int currentScore;

    int currentPlayerTagScore;

    int currentPlayerTagCount;

    float currentTime;
    float initTime;

    List<User> scores;

    User user;

    [SerializeField] private Text userNameText;
    [SerializeField] private Text scoreText;

    [SerializeField] private Text playerTagScoreText;

    [SerializeField] private Text playerTagCountText;
    [SerializeField] private Text timeText;

    [SerializeField] private GameObject newRecordPanel;

    [SerializeField] private GameObject RankingCanvas;


    void Start()
    {
        //プレイヤー名表示
        user = GameObject.Find("UserDataObject").GetComponent<User>();
        userNameText.text = user.GetUserName();

        //今回のゲームスコアを表示
        currentScore = PlayerPrefs.GetInt("score");
        scoreText.text = ($"Current Score: {currentScore}");

        currentPlayerTagCount = PlayerPrefs.GetInt("tagcount");
        playerTagCountText.text = ($"Player Tag: {currentPlayerTagCount}");

        currentPlayerTagScore = PlayerPrefs.GetInt("tagscore");
        playerTagScoreText.text = ($"Current PlayerTagScore: {currentPlayerTagScore}");

        currentTime = PlayerPrefs.GetFloat("time");
        initTime = PlayerPrefs.GetFloat("initTime");
        int minutes = (int)((initTime - currentTime) / 60.0f);
        int seconds = (int)((initTime - currentTime) % 60.0f);
        timeText.text = ($"Current Survival Time: {minutes}:{seconds}");

        scores = user.GetScores();
        // scores = CalcHighScore();

        if (CheckNewRecord())
        {
            newRecordPanel.SetActive(true);
        }
    }

    //ハイスコア生成用
    // int[] CalcHighScore()
    // {
    //     int[] newscores = new int[3];
    //     List<int> highscores = new List<int>();
    //     foreach (int s in scores)
    //     {
    //         highscores.Add(s);
    //     }
    //     highscores.Add(currentScore);

    //     for (int i = 0; i < 3; i++)
    //     {
    //         int highscore = highscores.Max();
    //         newscores.Append(highscore);
    //         highscores.Remove(i);
    //     }

    //     scores = newscores;

    //     return scores;
    // }

    bool CheckNewRecord()
    {
        bool isNewRecord = false;
        if (currentScore > scores[0].GetScore())
        {
            isNewRecord = true;
        }

        return isNewRecord;
    }
}
