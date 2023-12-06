using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class User : MonoBehaviour
{
    string user_name; //ユーザー名（userName）
    int total_score; //総スコア
    List<User> scores;
    float total_time_score; //総生存時間（totalTime）
    int total_kill_cnt; //総キル数（playerTagCount）
    int score; //1回のゲームスコア
    int kill_cnt; //1回のキル数
    string time_score; //1回の生存時間


    public string GetUserName()
    {
        return this.user_name;
    }

    public void SetUserName(string name)
    {
        this.user_name = name;
    }

    public int GetTotalScore()
    {
        return this.total_score;
    }

    public void SetTotalScore(int totalScore)
    {
        this.total_score = totalScore;
    }
    public List<User> GetScores()
    {
        return this.scores;
    }

    public void SetScores(List<User> scores)
    {
        this.scores = scores;
    }

    public int GetScoreOne(int i)
    {
        return this.scores[i].GetScore();
    }

    public int GetScore()
    {
        return this.score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public int GetPlayerTagCount()
    {
        return this.kill_cnt;
    }

    public int GetTotalPlayerTagCount()
    {
        return this.total_kill_cnt;
    }
    public void SetPlayerTagCount(int playerTagCount)
    {
        this.kill_cnt = playerTagCount;
    }

    public float GetTotalTime()
    {
        return this.total_time_score;
    }

    public void SetTotalTime(float totalTime)
    {
        this.total_time_score = totalTime;
    }

    public void SetSuvivalTime(float timer)
    {
        int minutes = (int)timer / 60;
        int seconds = (int)timer % 60;
        this.time_score = $"00:{minutes.ToString()}:{seconds.ToString()}";
    }
}
