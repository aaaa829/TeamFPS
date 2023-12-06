using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShowRanking : MonoBehaviour
{
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject scorePrefab;

    public ScoreUser[] userScores;

    public User[] users;

    public void Start()
    {
        DeleteScores();
        StartCoroutine(GetRanking());
        ShowingRanking();
    }

    public IEnumerator GetRanking()
    {
        string url = "http://localhost/FPS/get_ranking.py";

        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();
            switch (uwr.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + uwr.error);
                    break;
                default:
                    string responseText = uwr.downloadHandler.text;
                    Ranking ranking = JsonUtility.FromJson<Ranking>(responseText);
                    userScores = ranking.result;
                    Debug.Log(userScores[0].user_name);
                    break;
            }
        }
        makeRankingData();
    }



    public void ShowingRanking()
    {
        if (rankingPanel.gameObject.activeSelf)
        {
            rankingPanel.gameObject.SetActive(true);
        }
        else
        {
            rankingPanel.gameObject.SetActive(false);
        }
    }

    void makeRankingData()
    {
        Debug.Log(userScores.Length);
        for (int i = 0; i < userScores.Length; i++)
        {
            ScoreUser userScore = userScores[i];
            GameObject scorePre = Instantiate(scorePrefab, content);
            Text scoreText = scorePre.GetComponentInChildren<Text>();
            float times = (float)userScore.time_score / 100;
            int minutes = (int)(times / 60.0f);
            int seconds = (int)(times % 60.0f);
            scoreText.text = $"{i + 1:000}位 UserName: {userScore.user_name} Score: {userScore.score} \n Tag: {userScore.kill_cnt} Suvival Time: {minutes}:{seconds}";
        }
    }

    public void DeleteScores()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    public class Ranking
    {
        public ScoreUser[] result;
    }

    [Serializable]
    public class ScoreUser
    {
        public string user_name;
        public int score;
        public int kill_cnt;
        public int kill_score;
        public int item_cnt;
        public int item_score;
        public int time_score;
    }

}
